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
import { ActivatedRoute, Router } from '@angular/router';
import { StringResources } from '../../../../core/constants/string-resources';
import {
  validarCpfOuCnpj,
  validarEmailCliente
} from '../../../../core/validators/cliente.validators';
import { AuthSessionService } from '../../../../core/services/auth-session.service';
import { TipoLogin } from '../../models/login.model';
import { LoginService } from '../../services/login.service';

interface LoginContext {
  tipoLogin: TipoLogin;
  titulo: string;
  subtitulo: string;
  rotaSucesso: string;
}

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
  selector: 'app-login',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly loginService = inject(LoginService);
  private readonly authSessionService = inject(AuthSessionService);

  protected readonly isSubmitting = signal(false);
  protected readonly apiErrors = signal<string[]>([]);

  private readonly context = this.getContext();

  protected readonly titulo = this.context.titulo;
  protected readonly subtitulo = this.context.subtitulo;

  protected readonly form = this.formBuilder.nonNullable.group({
    identificador: ['', [identificadorValidator()]],
    senha: ['', [Validators.required]]
  });

  protected submit(): void {
    this.apiErrors.set([]);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);

    const payload = this.form.getRawValue();

    this.loginService
      .autenticar({
        identificador: payload.identificador.trim(),
        senha: payload.senha,
        tipoLogin: this.context.tipoLogin
      })
      .subscribe({
        next: (response) => {
          this.authSessionService.iniciarSessao(response);
          this.isSubmitting.set(false);
          this.router.navigateByUrl(this.context.rotaSucesso);
        },
        error: (error: HttpErrorResponse) => {
          const messages = Array.isArray(error.error)
            ? (error.error as string[])
            : [StringResources.LoginErroComunicacaoBackend];

          this.apiErrors.set(messages);
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

  private getContext(): LoginContext {
    const tipoInformado = Number(this.route.snapshot.data['tipoLogin']);
    const tipo = tipoInformado === TipoLogin.Gerencial ? TipoLogin.Gerencial : TipoLogin.Cliente;

    if (tipo === TipoLogin.Gerencial) {
      return {
        tipoLogin: TipoLogin.Gerencial,
        titulo: 'Login Gerencial',
        subtitulo: 'Acesso para a área administrativa.',
        rotaSucesso: '/gerencial'
      };
    }

    return {
      tipoLogin: TipoLogin.Cliente,
      titulo: 'Login Cliente',
      subtitulo: 'Acesso para clientes da plataforma.',
      rotaSucesso: '/cliente'
    };
  }
}
