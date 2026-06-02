import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { StringResources } from '../constants/string-resources';

const dddsValidos = new Set([
  11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 24, 27, 28, 31, 32, 33, 34, 35, 37, 38, 41, 42, 43,
  44, 45, 46, 47, 48, 49, 51, 53, 54, 55, 61, 62, 64, 63, 65, 66, 67, 68, 69, 71, 73, 74, 75, 77,
  79, 81, 87, 82, 83, 84, 85, 88, 86, 89, 91, 93, 94, 92, 97, 95, 96, 98, 99
]);

function apenasDigitos(valor: string): string {
  return valor.replace(/\D/g, '');
}

function validarCpf(cpf: string): boolean {
  if (cpf.length !== 11 || /^(\d)\1+$/.test(cpf)) {
    return false;
  }

  let soma = 0;
  for (let i = 0; i < 9; i++) {
    soma += Number(cpf[i]) * (10 - i);
  }

  let resto = soma % 11;
  const digito1 = resto < 2 ? 0 : 11 - resto;
  if (Number(cpf[9]) !== digito1) {
    return false;
  }

  soma = 0;
  for (let i = 0; i < 10; i++) {
    soma += Number(cpf[i]) * (11 - i);
  }

  resto = soma % 11;
  const digito2 = resto < 2 ? 0 : 11 - resto;
  return Number(cpf[10]) === digito2;
}

function validarCnpj(cnpj: string): boolean {
  if (cnpj.length !== 14 || /^(\d)\1+$/.test(cnpj)) {
    return false;
  }

  const pesos1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
  const pesos2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

  let soma = 0;
  for (let i = 0; i < 12; i++) {
    soma += Number(cnpj[i]) * pesos1[i];
  }

  let resto = soma % 11;
  const digito1 = resto < 2 ? 0 : 11 - resto;
  if (Number(cnpj[12]) !== digito1) {
    return false;
  }

  soma = 0;
  for (let i = 0; i < 13; i++) {
    soma += Number(cnpj[i]) * pesos2[i];
  }

  resto = soma % 11;
  const digito2 = resto < 2 ? 0 : 11 - resto;
  return Number(cnpj[13]) === digito2;
}

function ehSequenciaNumerica(valor: string, passo: 1 | -1): boolean {
  for (let i = 1; i < valor.length; i++) {
    if (Number(valor[i]) - Number(valor[i - 1]) !== passo) {
      return false;
    }
  }

  return true;
}

function contemNumerosSequenciais(valor: string, tamanhoMinimo = 4): boolean {
  if (valor.length < tamanhoMinimo) {
    return false;
  }

  for (let i = 0; i <= valor.length - tamanhoMinimo; i++) {
    const trecho = valor.slice(i, i + tamanhoMinimo);

    if (!/^\d+$/.test(trecho)) {
      continue;
    }

    if (ehSequenciaNumerica(trecho, 1) || ehSequenciaNumerica(trecho, -1)) {
      return true;
    }
  }

  return false;
}

export function validarEmailCliente(email: string): boolean {
  const valor = email.trim();
  const pattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/i;
  return pattern.test(valor);
}

export function validarCpfOuCnpj(valor: string): boolean {
  const documento = apenasDigitos(valor);

  if (documento.length === 11) {
    return validarCpf(documento);
  }

  if (documento.length === 14) {
    return validarCnpj(documento);
  }

  return false;
}

export function nomeCompletoValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valor = String(control.value ?? '').trim();

    if (!valor) {
      return { required: { message: StringResources.ClienteNomeObrigatorio } };
    }

    const partes = valor.split(/\s+/).filter(Boolean);

    if (partes.length < 2) {
      return { nomeIncompleto: { message: StringResources.ClienteNomeDeveConterMaisDeUmNome } };
    }

    const partesValidas = partes.every(
      (parte) => parte.length >= 2 && /^[\p{L}'-]+$/u.test(parte)
    );

    return partesValidas
      ? null
      : { nomeIncompleto: { message: StringResources.ClienteNomeDeveConterMaisDeUmNome } };
  };
}

export function emailClienteValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valor = String(control.value ?? '').trim();

    if (!valor) {
      return { required: { message: StringResources.ClienteEmailObrigatorio } };
    }

    return validarEmailCliente(valor)
      ? null
      : { emailInvalido: { message: StringResources.ClienteEmailInvalido } };
  };
}

export function telefoneValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valor = String(control.value ?? '').trim();

    if (!valor) {
      return { required: { message: StringResources.ClienteTelefoneObrigatorio } };
    }

    const telefone = apenasDigitos(valor);

    if (telefone.length > 11) {
      return { telefoneMaximo: { message: StringResources.ClienteTelefoneMaximoCaracteres } };
    }

    if (telefone.length !== 10 && telefone.length !== 11) {
      return { telefoneInvalido: { message: StringResources.ClienteTelefoneInvalido } };
    }

    const ddd = Number(telefone.slice(0, 2));
    if (!dddsValidos.has(ddd)) {
      return { telefoneInvalido: { message: StringResources.ClienteTelefoneInvalido } };
    }

    if (telefone.length === 11 && telefone[2] !== '9') {
      return { telefoneInvalido: { message: StringResources.ClienteTelefoneInvalido } };
    }

    const numeroSemDdd = telefone.slice(2);
    if (/^(\d)\1+$/.test(numeroSemDdd)) {
      return { telefoneInvalido: { message: StringResources.ClienteTelefoneInvalido } };
    }

    return null;
  };
}

export function cpfCnpjValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valor = String(control.value ?? '').trim();

    if (!valor) {
      return { required: { message: StringResources.ClienteCpfCnpjObrigatorio } };
    }

    return validarCpfOuCnpj(valor)
      ? null
      : { cpfCnpjInvalido: { message: StringResources.ClienteCpfCnpjInvalido } };
  };
}

export function senhaClienteValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const senha = String(control.value ?? '');

    if (!senha) {
      return { required: { message: StringResources.ClienteSenhaObrigatoria } };
    }

    const erros: ValidationErrors = {};

    if (senha.length < 10) {
      erros['senhaMinimo'] = { message: StringResources.SenhaDeveTerMinimoCaracteres };
    }

    if (!/[A-Za-z]/.test(senha)) {
      erros['senhaLetras'] = { message: StringResources.ClienteSenhaDeveConterLetras };
    }

    if (!/[A-Z]/.test(senha)) {
      erros['senhaMaiuscula'] = { message: StringResources.ClienteSenhaDeveConterLetraMaiuscula };
    }

    if (!/[^A-Za-z0-9]/.test(senha)) {
      erros['senhaEspecial'] = { message: StringResources.ClienteSenhaDeveConterCaractereEspecial };
    }

    if (contemNumerosSequenciais(senha)) {
      erros['senhaSequencial'] = { message: StringResources.ClienteSenhaNaoPodeConterNumerosSequenciais };
    }

    return Object.keys(erros).length > 0 ? erros : null;
  };
}

export function obterMensagemErro(control: AbstractControl | null): string | null {
  if (!control?.errors || !(control.touched || control.dirty)) {
    return null;
  }

  for (const erro of Object.values(control.errors)) {
    if (typeof erro === 'object' && erro !== null && 'message' in erro) {
      return String((erro as { message: string }).message);
    }
  }

  if (control.errors['maxlength']) {
    const maxLength = control.errors['maxlength'].requiredLength;

    if (maxLength === 150 && control.parent?.get('email') === control) {
      return StringResources.ClienteEmailMaximoCaracteres;
    }

    if (maxLength === 150) {
      return StringResources.ClienteNomeMaximoCaracteres;
    }

    if (maxLength === 500) {
      return StringResources.ClienteObservacaoMaximoCaracteres;
    }
  }

  return null;
}
