import { Injectable, signal } from '@angular/core';

export type ToastTipo = 'sucesso' | 'alerta' | 'erro';

export interface ToastMensagem {
  id: number;
  tipo: ToastTipo;
  texto: string;
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private readonly duracaoPadraoMs = 4000;
  private readonly timeouts = new Map<number, number>();
  private proximoId = 1;

  readonly mensagens = signal<ToastMensagem[]>([]);

  sucesso(texto: string): void {
    this.mostrar(texto, 'sucesso');
  }

  alerta(texto: string): void {
    this.mostrar(texto, 'alerta');
  }

  erro(texto: string): void {
    this.mostrar(texto, 'erro');
  }

  mostrar(texto: string, tipo: ToastTipo, duracaoMs: number = this.duracaoPadraoMs): void {
    const id = this.proximoId++;
    const mensagem: ToastMensagem = { id, tipo, texto };

    this.mensagens.update((lista) => [...lista, mensagem]);

    const timeoutId = globalThis.setTimeout(() => this.remover(id), duracaoMs);
    this.timeouts.set(id, timeoutId);
  }

  remover(id: number): void {
    this.mensagens.update((lista) => lista.filter((item) => item.id !== id));

    const timeoutId = this.timeouts.get(id);
    if (timeoutId !== undefined) {
      clearTimeout(timeoutId);
      this.timeouts.delete(id);
    }
  }
}
