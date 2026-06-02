import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { StringResources } from '../../../../core/constants/string-resources';
import {
  cpfCnpjValidator,
  emailClienteValidator,
  nomeCompletoValidator,
  obterMensagemErro,
  senhaClienteValidator,
  telefoneValidator
} from '../../../../core/validators/cliente.validators';
import { CadastroClienteService } from '../../services/cadastro-cliente.service';

@Component({
  selector: 'app-cadastro-cliente',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule],
  templateUrl: './cadastro-cliente.component.html',
  styleUrls: ['./cadastro-cliente.component.scss']
})
export class CadastroClienteComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly cadastroClienteService = inject(CadastroClienteService);

  protected readonly isSubmitting = signal(false);
  protected readonly apiErrors = signal<string[]>([]);
  protected readonly successMessage = signal<string | null>(null);

  protected readonly form = this.formBuilder.nonNullable.group({
    nome: ['', [nomeCompletoValidator(), Validators.maxLength(150)]],
    email: ['', [emailClienteValidator(), Validators.maxLength(150)]],
    telefone: ['', [telefoneValidator()]],
    cpfCnpj: ['', [cpfCnpjValidator()]],
    senha: ['', [senhaClienteValidator()]],
    observacao: ['', [Validators.maxLength(500)]]
  });

  protected fieldError(
    controlName: 'nome' | 'email' | 'telefone' | 'cpfCnpj' | 'senha' | 'observacao'
  ): string | null {
    const control = this.form.get(controlName);

    if (!control) {
      return null;
    }

    const mensagemCustomizada = obterMensagemErro(control);
    if (mensagemCustomizada) {
      return mensagemCustomizada;
    }

    return null;
  }

  protected submit(): void {
    this.successMessage.set(null);
    this.apiErrors.set([]);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);

    const payload = this.form.getRawValue();

    this.cadastroClienteService
      .cadastrar({
        nome: payload.nome.trim(),
        email: payload.email.trim(),
        telefone: payload.telefone.trim() || null,
        cpfCnpj: payload.cpfCnpj.trim(),
        senha: payload.senha,
        observacao: payload.observacao.trim() || null
      })
      .subscribe({
        next: (response) => {
          this.successMessage.set(
            StringResources.ClienteCadastroSucesso.replace('{0}', String(response.clienteId))
          );
          this.form.reset();
          this.isSubmitting.set(false);
        },
        error: (error: HttpErrorResponse) => {
          const messages = Array.isArray(error.error)
            ? (error.error as string[])
            : [StringResources.ClienteErroComunicacaoBackend];

          this.apiErrors.set(messages);
          this.isSubmitting.set(false);
        }
      });
  }
}
