import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { StringResources } from '../../../../core/constants/string-resources';
import {
  confirmarSenhaObrigatoriaValidator,
  cpfCnpjValidator,
  emailClienteValidator,
  nomeCompletoValidator,
  obterMensagemErro,
  senhaClienteValidator,
  telefoneValidator
} from '../../../../core/validators/cliente.validators';
import { ToastMensagemService } from '../../../../core/services/toast.service';
import { CadastroClienteService } from '../../services/cadastro-cliente.service';

type CadastroClienteForm = {
  cpfCnpj: string;
  nome: string;
  telefone: string;
  email: string;
  senha: string;
  confirmarSenha: string;
};

@Component({
  selector: 'app-cadastro-cliente',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './cadastro-cliente.component.html',
  styleUrls: ['./cadastro-cliente.component.scss']
})
export class CadastroClienteComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly cadastroClienteService = inject(CadastroClienteService);
  private readonly toastMensagemService = inject(ToastMensagemService);

  protected readonly isSubmitting = signal(false);
  protected readonly apiErrors = signal<string[]>([]);
  protected readonly successMessage = signal<string | null>(null);
  protected readonly tipoConta = signal<'cliente' | 'administrador'>('cliente');

  protected readonly form = this.formBuilder.nonNullable.group(
    {
      cpfCnpj: ['', [cpfCnpjValidator()]],
      nome: ['', [nomeCompletoValidator(), Validators.maxLength(150)]],
      telefone: ['', [telefoneValidator()]],
      email: ['', [emailClienteValidator(), Validators.maxLength(150)]],
      senha: ['', [senhaClienteValidator()]],
      confirmarSenha: ['', [confirmarSenhaObrigatoriaValidator()]]
    },
    {
      validators: [this.validarSenhasIguais()]
    }
  );

  protected fieldError(
    controlName: keyof CadastroClienteForm
  ): string | null {
    const control = this.form.get(controlName);

    if (!control) {
      return null;
    }

    const mensagemCustomizada = obterMensagemErro(control);
    if (mensagemCustomizada) {
      return mensagemCustomizada;
    }

    if (
      controlName === 'confirmarSenha' &&
      this.form.hasError('senhasDiferentes') &&
      (control.touched || control.dirty)
    ) {
      return StringResources.ClienteConfirmacaoSenhaInvalida;
    }

    return null;
  }

  protected isConfirmarSenhaInvalida(): boolean {
    const control = this.form.controls.confirmarSenha;

    return (control.touched || control.dirty) && this.form.hasError('senhasDiferentes');
  }

  protected setTipoConta(tipo: 'cliente' | 'administrador'): void {
    this.tipoConta.set(tipo);
  }

  private validarSenhasIguais() {
    return () => {
      const senha = this.form?.controls.senha.value ?? '';
      const confirmarSenha = this.form?.controls.confirmarSenha.value ?? '';

      if (!senha || !confirmarSenha || senha === confirmarSenha) {
        return null;
      }

      return {
        senhasDiferentes: true
      };
    };
  }

  protected submit(): void {
    this.successMessage.set(null);
    this.apiErrors.set([]);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toastMensagemService.alerta(StringResources.ClienteCadastroFormularioInvalido);
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
        observacao: null
      })
      .subscribe({
        next: (response) => {
          const mensagemSucesso = StringResources.ClienteCadastroSucesso.replace('{0}', String(response.clienteId));

          this.successMessage.set(mensagemSucesso);
          this.toastMensagemService.sucesso(mensagemSucesso);
          this.form.reset();
          this.isSubmitting.set(false);
        },
        error: (error: HttpErrorResponse) => {
          const messages = Array.isArray(error.error)
            ? (error.error as string[])
            : [StringResources.ClienteErroComunicacaoBackend];

          this.apiErrors.set(messages);
          this.toastMensagemService.erro(messages[0] ?? StringResources.ClienteErroComunicacaoBackend);
          this.isSubmitting.set(false);
        }
      });
  }
}
