import { NgOptimizedImage } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter } from 'rxjs';

import { LoadingService } from './core/services/loading.service';

@Component({
  selector: 'app-root',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NgOptimizedImage, RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private readonly appName = signal('BlueClean');
  private readonly router = inject(Router);
  private readonly loadingService = inject(LoadingService);
  private readonly currentUrl = signal(this.router.url);
  protected readonly anoAtual = new Date().getFullYear();
  protected readonly title = computed(() => `${this.appName()} Proxy`);
  protected readonly isLoading = this.loadingService.isLoading;
  protected readonly showSidebar = computed(() =>
    this.isRestrictedRoute(this.currentUrl())
  );
  protected readonly navItems = [
    { label: 'Inicio', path: '/' },
    { label: 'Cadastro Cliente', path: '/cadastroCliente' },
    { label: 'Login Cliente', path: '/login/cliente' },
    { label: 'Login Gerencial', path: '/login/gerencial' },
    { label: 'Cliente', path: '/cliente' },
    { label: 'Gerencial', path: '/gerencial' }
  ] as const;

  constructor() {
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd),
        takeUntilDestroyed()
      )
      .subscribe((event) => this.currentUrl.set(event.urlAfterRedirects));
  }

  private isRestrictedRoute(url: string): boolean {
    return url.startsWith('/cliente') || url.startsWith('/gerencial');
  }
}
