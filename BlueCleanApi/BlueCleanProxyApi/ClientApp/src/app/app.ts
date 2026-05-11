import { ChangeDetectionStrategy, Component, computed, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  private readonly appName = signal('BlueClean');
  protected readonly title = computed(() => `${this.appName()} Proxy`);
  protected readonly navItems = [
    { label: 'Inicio', path: '/' },
    { label: 'Login', path: '/login' }
  ] as const;
}
