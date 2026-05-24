import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {
  ClienteCadastroRequest,
  ClienteCadastroResponse
} from '../models/cliente-cadastro.model';

@Injectable({ providedIn: 'root' })
export class ClienteService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/Cliente`;

  cadastrar(request: ClienteCadastroRequest): Observable<ClienteCadastroResponse> {
    return this.http.post<ClienteCadastroResponse>(`${this.baseUrl}/Cadastrar`, request);
  }
}
