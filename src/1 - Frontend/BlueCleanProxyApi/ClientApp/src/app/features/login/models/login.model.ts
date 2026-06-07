export enum TipoLogin {
  Cliente = 1,
  Gerencial = 2
}

export interface LoginRequest {
  identificador: string;
  senha: string;
  tipoLogin: TipoLogin;
}

export interface LoginResponse {
  token: string;
  nomeUsuario: string;
  tipoLogin: TipoLogin;
  expiraEmUtc: string;
}

export interface AuthSession {
  token: string;
  nomeUsuario: string;
  tipoLogin: TipoLogin;
  expiraEmUtc: string;
}
