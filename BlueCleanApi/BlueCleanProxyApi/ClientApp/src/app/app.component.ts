import { NgOptimizedImage } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

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
  private readonly loadingService = inject(LoadingService);
  protected readonly title = computed(() => `${this.appName()} Proxy`);
  protected readonly isLoading = this.loadingService.isLoading;
  protected readonly navItems = [
    { label: 'Inicio', path: '/' },
    { label: 'Cadastro Cliente', path: '/cadastroCliente' },
    { label: 'Login Cliente', path: '/login/cliente' },
    { label: 'Login Gerencial', path: '/login/gerencial' },
    { label: 'Modulo Cliente', path: '/cliente' },
    { label: 'Modulo Gerencial', path: '/modulo-gerencial' }
  ] as const;
}
