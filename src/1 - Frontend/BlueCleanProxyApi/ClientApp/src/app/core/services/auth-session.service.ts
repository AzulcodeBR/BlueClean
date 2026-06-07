import { computed, Injectable, signal } from '@angular/core';
import { AuthSession, LoginResponse, TipoLogin } from '../../features/login/models/login.model';

@Injectable({ providedIn: 'root' })
export class AuthSessionService {
  private readonly storageKey = 'blueclean.auth.session';
  private readonly sessionState = signal<AuthSession | null>(this.loadSession());

  readonly session = computed(() => this.getSession());
  readonly nomeUsuario = computed(() => this.session()?.nomeUsuario ?? null);

  iniciarSessao(response: LoginResponse): void {
    const session: AuthSession = {
      token: response.token,
      nomeUsuario: response.nomeUsuario,
      tipoLogin: response.tipoLogin,
      expiraEmUtc: response.expiraEmUtc
    };

    this.sessionState.set(session);
    sessionStorage.setItem(this.storageKey, JSON.stringify(session));
  }

  encerrarSessao(): void {
    this.sessionState.set(null);
    sessionStorage.removeItem(this.storageKey);
  }

  isAuthenticatedFor(tipoLogin: TipoLogin): boolean {
    const session = this.getSession();

    if (!session) {
      return false;
    }

    return session.tipoLogin === tipoLogin;
  }

  private getSession(): AuthSession | null {
    const session = this.sessionState();

    if (!session) {
      return null;
    }

    if (this.isExpired(session.expiraEmUtc)) {
      this.encerrarSessao();
      return null;
    }

    return session;
  }

  private loadSession(): AuthSession | null {
    const storedSession = sessionStorage.getItem(this.storageKey);

    if (!storedSession) {
      return null;
    }

    try {
      const parsedSession = JSON.parse(storedSession) as AuthSession;

      if (this.isExpired(parsedSession.expiraEmUtc)) {
        sessionStorage.removeItem(this.storageKey);
        return null;
      }

      return parsedSession;
    } catch {
      sessionStorage.removeItem(this.storageKey);
      return null;
    }
  }

  private isExpired(expiraEmUtc: string): boolean {
    const expirationTime = Date.parse(expiraEmUtc);

    if (Number.isNaN(expirationTime)) {
      return true;
    }

    return expirationTime <= Date.now();
  }
}
