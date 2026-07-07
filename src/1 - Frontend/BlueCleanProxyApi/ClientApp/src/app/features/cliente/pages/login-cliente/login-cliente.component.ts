import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { StringResources } from '../../../../core/constants/string-resources';
import { SessionService } from '../../../../core/services/session.service';
import { ToastService } from '../../../../core/services/toast.service';
import {
  validarCpfOuCnpj,
  validarEmailCliente
} from '../../../../core/validators/cliente.validators';
import { TipoLogin } from '../../../../core/models/login.model';
import { LoginService } from '../../../../core/services/login.service';

function identificadorValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valor = String(control.value ?? '').trim();

    if (!valor) {
      return { required: true };
    }

    if (validarEmailCliente(valor) || validarCpfOuCnpj(valor)) {
      return null;
    }

    return { identificadorInvalido: true };
  };
}

@Component({
  selector: 'app-login-cliente',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-cliente.component.html',
  styleUrls: ['./login-cliente.component.scss']
})
export class LoginClienteComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly loginService = inject(LoginService);
  private readonly sessionService = inject(SessionService);
  private readonly toastService = inject(ToastService);

  protected readonly tipoLogin = TipoLogin.Cliente;
  protected readonly isSubmitting = signal(false);
  protected readonly apiErrors = signal<string[]>([]);

  protected readonly form = this.formBuilder.nonNullable.group({
    identificador: ['', [identificadorValidator()]],
    senha: ['', [Validators.required]]
  });

  protected submit(): void {
    this.apiErrors.set([]);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toastService.alerta(StringResources.LoginFormularioInvalido);
      return;
    }

    this.isSubmitting.set(true);

    const payload = this.form.getRawValue();

    this.loginService
      .autenticar({
        identificador: payload.identificador.trim(),
        senha: payload.senha,
        tipoLogin: this.tipoLogin
      })
      .subscribe({
        next: (response) => {
          this.sessionService.criarSessao(response);
          this.toastService.sucesso(StringResources.LoginSucesso);
          this.isSubmitting.set(false);
          this.router.navigateByUrl('/cliente');
        },
        error: (error: HttpErrorResponse) => {
          const messages = Array.isArray(error.error)
            ? (error.error as string[])
            : [StringResources.LoginErroComunicacaoBackend];

          this.apiErrors.set(messages);
          this.toastService.erro(messages[0] ?? StringResources.LoginErroComunicacaoBackend);
          this.isSubmitting.set(false);
        }
      });
  }

  protected fieldError(controlName: 'identificador' | 'senha'): string | null {
    const control = this.form.get(controlName);

    if (!control?.errors || !(control.touched || control.dirty)) {
      return null;
    }

    if (control.errors['required']) {
      if (controlName === 'identificador') {
        return StringResources.LoginIdentificadorObrigatorio;
      }

      return StringResources.LoginSenhaObrigatoria;
    }

    if (control.errors['identificadorInvalido']) {
      return StringResources.LoginIdentificadorInvalido;
    }

    return null;
  }
}
