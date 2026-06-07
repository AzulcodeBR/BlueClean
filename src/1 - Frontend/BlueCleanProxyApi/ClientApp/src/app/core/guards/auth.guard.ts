import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TipoLogin } from '../../features/login/models/login.model';
import { AuthSessionService } from '../services/auth-session.service';

export const authGuard: CanActivateFn = (route) => {
  const authSessionService = inject(AuthSessionService);
  const router = inject(Router);

  const tipoInformado = Number(route.data['tipoLogin']);
  const tipoLogin =
    tipoInformado === TipoLogin.Gerencial ? TipoLogin.Gerencial : TipoLogin.Cliente;

  if (authSessionService.isAuthenticatedFor(tipoLogin)) {
    return true;
  }

  if (tipoLogin === TipoLogin.Gerencial) {
    return router.createUrlTree(['/login/gerencial']);
  }

  return router.createUrlTree(['/login/cliente']);
};
