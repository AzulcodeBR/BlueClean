import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./features/home/pages/home/home.component').then((m) => m.HomeComponent)
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/login/pages/login/login.component').then((m) => m.LoginComponent)
  },
  {
    path: 'cliente',
    loadComponent: () =>
      import('./features/cliente/pages/cliente/cliente.component').then((m) => m.ClienteComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
