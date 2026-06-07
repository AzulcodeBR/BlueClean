export interface ClienteCadastroRequest {
  nome: string;
  email: string;
  telefone?: string | null;
  cpfCnpj: string;
  senha: string;
  observacao?: string | null;
}

export interface ClienteCadastroResponse {
  clienteId: number;
}
