import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { TipoLogin } from './features/login/models/login.model';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login/gerencial',
    pathMatch: 'full'
  },
  {
    path: 'login',
    redirectTo: 'login/cliente',
    pathMatch: 'full'
  },
  {
    path: 'login/cliente',
    data: { tipoLogin: TipoLogin.Cliente },
    loadComponent: () =>
      import('./features/login/pages/login/login.component').then((m) => m.LoginComponent)
  },
  {
    path: 'login/gerencial',
    data: { tipoLogin: TipoLogin.Gerencial },
    loadComponent: () =>
      import('./features/login/pages/login/login.component').then((m) => m.LoginComponent)
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
    path: 'register',
    loadComponent: () =>
      import('./features/cliente/pages/cadastro-cliente/cadastro-cliente.component').then(
        (m) => m.CadastroClienteComponent
      )
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
