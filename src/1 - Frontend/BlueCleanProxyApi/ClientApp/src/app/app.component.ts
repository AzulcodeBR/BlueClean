import { NgOptimizedImage } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter } from 'rxjs';

import { SessionService } from './core/services/session.service';
import { LoadingService } from './core/services/loading.service';
import { ToastService } from './core/services/toast.service';
import { TipoLogin } from './core/models/login.model';

interface SidebarItem {
  label: string;
  path: string;
}

interface PageMeta {
  kicker: string;
  title: string;
  subtitle: string;
}

@Component({
  selector: 'app-root',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NgOptimizedImage, RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private readonly router = inject(Router);
  private readonly loadingService = inject(LoadingService);
  private readonly toastService = inject(ToastService);
  private readonly sessionService = inject(SessionService);
  private readonly urlAtual = signal(this.router.url);

  protected readonly isLoading = this.loadingService.isLoading;
  protected readonly toastMensagens = this.toastService.mensagens;
  protected readonly sessaoAutenticada = this.sessionService.sessaoAutenticada;
  protected readonly nomeUsuario = computed(() => this.sessionService.nomeUsuario() ?? 'Usuario');
  protected readonly tipoUsuario = computed(() =>
    this.sessaoAutenticada()?.tipoLogin === TipoLogin.Gerencial ? 'Administrador' : 'Cliente'
  );
  protected readonly showPrivateShell = computed(() => this.mustShowPrivateShell(this.urlAtual()));
  protected readonly pageMeta = computed(() => this.getPageMeta(this.urlAtual()));
  protected readonly sidebarItems = computed<SidebarItem[]>(() => this.getSidebarItems());

  constructor() {
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd),
        takeUntilDestroyed()
      )
      .subscribe((event) => this.urlAtual.set(event.urlAfterRedirects));
  }

  protected logout(): void {
    const tipoAtual = this.sessaoAutenticada()?.tipoLogin;

    this.sessionService.encerrarSessao();

    if (tipoAtual === TipoLogin.Gerencial) {
      this.router.navigateByUrl('/gerencial/login');
      return;
    }

    this.router.navigateByUrl('/cliente/login');
  }

  protected removerToast(id: number): void {
    this.toastService.remover(id);
  }

  private mustShowPrivateShell(url: string): boolean {
    if (!this.sessaoAutenticada()) {
      return false;
    }

    return !url.endsWith('/login') && !url.startsWith('/cliente/cadastro');
  }

  private getSidebarItems(): SidebarItem[] {
    const baseItems: SidebarItem[] = [
      {
        label: 'Dashboard',
        path: this.sessaoAutenticada()?.tipoLogin === TipoLogin.Gerencial ? '/gerencial' : '/cliente'
      }
    ];

    if (this.sessaoAutenticada()?.tipoLogin === TipoLogin.Gerencial) {
      baseItems.push({
        label: 'Clientes',
        path: '/cadastroCliente'
      });
    }

    baseItems.push({
      label: 'Voltar ao login',
      path:
        this.sessaoAutenticada()?.tipoLogin === TipoLogin.Gerencial
          ? '/gerencial/login'
          : '/cliente/login'
    });

    return baseItems;
  }

  private getPageMeta(url: string): PageMeta {
    if (url.startsWith('/gerencial')) {
      return {
        kicker: 'Dashboard',
        title: `Olá, ${this.primeiroNome(this.nomeUsuario())}`,
        subtitle: 'Visao geral da sua operação.'
      };
    }

    if (url.startsWith('/cliente')) {
      return {
        kicker: 'Area do cliente',
        title: `Bem-vindo, ${this.primeiroNome(this.nomeUsuario())}`,
        subtitle: 'Acompanhe seus acessos e dados de cadastro.'
      };
    }

    if (url.startsWith('/cadastroCliente') || url.startsWith('/cliente/cadastro')) {
      return {
        kicker: 'Cadastros',
        title: 'Novo cliente',
        subtitle: 'Preencha os campos para criar uma nova conta.'
      };
    }

    return {
      kicker: 'BlueClean',
      title: 'BlueClean | Lavanderia SaaS',
      subtitle: 'Gestao moderna para sua operacao.'
    };
  }

  private primeiroNome(nomeCompleto: string): string {
    return nomeCompleto.trim().split(' ')[0] ?? 'Usuario';
  }
}
