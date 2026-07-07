import { computed, Injectable, signal } from '@angular/core';
import { AuthSession, LoginResponse, TipoLogin } from '../models/login.model';

@Injectable({ providedIn: 'root' })
export class SessionService {
  private readonly storageChaveAuth = 'blueclean.auth.session';
  private readonly authSession = signal<AuthSession | null>(this.carregarSessao());

  readonly sessaoAutenticada = computed(() => this.obterSessao());
  readonly nomeUsuario = computed(() => this.sessaoAutenticada()?.nomeUsuario ?? null);

  criarSessao(response: LoginResponse): void {
    const novaSessao: AuthSession = {
      token: response.token,
      nomeUsuario: response.nomeUsuario,
      tipoLogin: response.tipoLogin,
      expiraEmUtc: response.expiraEmUtc
    };

    this.authSession.set(novaSessao);
    sessionStorage.setItem(this.storageChaveAuth, JSON.stringify(novaSessao));
  }

  encerrarSessao(): void {
    this.authSession.set(null);
    sessionStorage.removeItem(this.storageChaveAuth);
  }

  verificarSessaoTipoLogin(tipoLogin: TipoLogin): boolean {
    const sessaoExistente = this.obterSessao();

    if (!sessaoExistente) {
      return false;
    }

    return sessaoExistente.tipoLogin === tipoLogin;
  }

  private obterSessao(): AuthSession | null {
    const session = this.carregarSessao();

    if (!session) {
      return null;
    }

    if (this.verificarSessaoExpirada(session.expiraEmUtc)) {
      this.encerrarSessao();
      return null;
    }

    return session;
  }

  private carregarSessao(): AuthSession | null {
    const sessaoJson = sessionStorage.getItem(this.storageChaveAuth);

    if (!sessaoJson) {
      return null;
    }

    try {
      const sessaoExistente = JSON.parse(sessaoJson) as AuthSession;

      if (this.verificarSessaoExpirada(sessaoExistente.expiraEmUtc)) {
        sessionStorage.removeItem(this.storageChaveAuth);
        return null;
      }

      return sessaoExistente;
    } catch {
      sessionStorage.removeItem(this.storageChaveAuth);
      return null;
    }
  }

  private verificarSessaoExpirada(expiraEmUtc: string): boolean {
    const dataHoraExpiracao = Date.parse(expiraEmUtc);

    if (Number.isNaN(dataHoraExpiracao)) {
      return true;
    }

    return dataHoraExpiracao <= Date.now();
  }
}
