import { NgOptimizedImage } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter } from 'rxjs';

import { AuthSessionService } from './core/services/auth-session.service';
import { LoadingService } from './core/services/loading.service';
import { TipoLogin } from './features/login/models/login.model';

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
  private readonly authSessionService = inject(AuthSessionService);
  private readonly currentUrl = signal(this.router.url);

  protected readonly isLoading = this.loadingService.isLoading;
  protected readonly session = this.authSessionService.session;
  protected readonly nomeUsuario = computed(() => this.authSessionService.nomeUsuario() ?? 'Usuario');
  protected readonly tipoUsuario = computed(() =>
    this.session()?.tipoLogin === TipoLogin.Gerencial ? 'Administrador' : 'Cliente'
  );
  protected readonly showPrivateShell = computed(() => this.mustShowPrivateShell(this.currentUrl()));
  protected readonly pageMeta = computed(() => this.getPageMeta(this.currentUrl()));
  protected readonly sidebarItems = computed<SidebarItem[]>(() => this.getSidebarItems());

  constructor() {
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd),
        takeUntilDestroyed()
      )
      .subscribe((event) => this.currentUrl.set(event.urlAfterRedirects));
  }

  protected logout(): void {
    const tipoAtual = this.session()?.tipoLogin;

    this.authSessionService.encerrarSessao();

    if (tipoAtual === TipoLogin.Gerencial) {
      this.router.navigateByUrl('/login/gerencial');
      return;
    }

    this.router.navigateByUrl('/login/cliente');
  }

  private mustShowPrivateShell(url: string): boolean {
    if (!this.session()) {
      return false;
    }

    return !url.startsWith('/login') && !url.startsWith('/register');
  }

  private getSidebarItems(): SidebarItem[] {
    const baseItems: SidebarItem[] = [
      {
        label: 'Dashboard',
        path: this.session()?.tipoLogin === TipoLogin.Gerencial ? '/gerencial' : '/cliente'
      }
    ];

    if (this.session()?.tipoLogin === TipoLogin.Gerencial) {
      baseItems.push({
        label: 'Clientes',
        path: '/cadastroCliente'
      });
    }

    baseItems.push({
      label: 'Voltar ao login',
      path: this.session()?.tipoLogin === TipoLogin.Gerencial ? '/login/gerencial' : '/login/cliente'
    });

    return baseItems;
  }

  private getPageMeta(url: string): PageMeta {
    if (url.startsWith('/gerencial')) {
      return {
        kicker: 'Dashboard',
        title: `Ola, ${this.primeiroNome(this.nomeUsuario())}`,
        subtitle: 'Visao geral da sua operacao.'
      };
    }

    if (url.startsWith('/cliente')) {
      return {
        kicker: 'Area do cliente',
        title: `Bem-vindo, ${this.primeiroNome(this.nomeUsuario())}`,
        subtitle: 'Acompanhe seus acessos e dados de cadastro.'
      };
    }

    if (url.startsWith('/cadastroCliente') || url.startsWith('/register')) {
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
