import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TipoLogin } from '../models/login.model';
import { SessionService } from '../services/session.service';

export const authGuard: CanActivateFn = (route) => {
  const sessionService = inject(SessionService);
  const router = inject(Router);

  const tipoInformado = Number(route.data['tipoLogin']);
  const tipoLogin =
    tipoInformado === TipoLogin.Gerencial ? TipoLogin.Gerencial : TipoLogin.Cliente;

  if (sessionService.verificarSessaoTipoLogin(tipoLogin)) {
    return true;
  }

  if (tipoLogin === TipoLogin.Gerencial) {
    return router.createUrlTree(['/gerencial/login']);
  }

  return router.createUrlTree(['/cliente/login']);
};
