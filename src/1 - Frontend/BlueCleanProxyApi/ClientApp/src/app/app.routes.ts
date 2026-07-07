import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { TipoLogin } from './core/models/login.model';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'gerencial/login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    redirectTo: 'cliente/login',
    pathMatch: 'full'
  },
  {
    path: 'cliente/login',
    data: { tipoLogin: TipoLogin.Cliente },
    loadComponent: () =>
      import('./features/cliente/pages/login-cliente/login-cliente.component').then(
        (m) => m.LoginClienteComponent
      )
  },
  {
    path: 'gerencial/login',
    data: { tipoLogin: TipoLogin.Gerencial },
    loadComponent: () =>
      import('./features/gerencial/pages/login-gerencial/login-gerencial.component').then(
        (m) => m.LoginGerencialComponent
      )
  },
  {
    path: 'login/cliente',
    redirectTo: 'cliente/login',
    pathMatch: 'full'
  },
  {
    path: 'login/gerencial',
    redirectTo: 'gerencial/login',
    pathMatch: 'full'
  },
  {
    path: 'cliente',
    canActivate: [authGuard],
    data: { tipoLogin: TipoLogin.Cliente },
    loadComponent: () =>
      import('./features/cliente/pages/cliente/cliente.component').then(
        (m) => m.ClienteComponent
      )
  },
  {
    path: 'gerencial',
    canActivate: [authGuard],
    data: { tipoLogin: TipoLogin.Gerencial },
    loadComponent: () =>
      import('./features/gerencial/pages/gerencial/gerencial.component').then(
        (m) => m.GerencialComponent
      )
  },
  {
    path: 'cliente/cadastro',
    loadComponent: () =>
      import('./features/cliente/pages/cadastro-cliente/cadastro-cliente.component').then(
        (m) => m.CadastroClienteComponent
      )
  },
  {
    path: 'register',
    redirectTo: 'cliente/cadastro',
    pathMatch: 'full'
  },
  {
    path: 'cadastroCliente',
    loadComponent: () =>
      import('./features/cliente/pages/cadastro-cliente/cadastro-cliente.component').then(
        (m) => m.CadastroClienteComponent
      )
  },
  {
    path: '**',
    redirectTo: ''
  }
];
