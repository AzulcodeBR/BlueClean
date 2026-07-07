import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse } from '../models/login.model';

@Injectable({ providedIn: 'root' })
export class LoginService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/Login`;

  autenticar(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/Autenticar`, request);
  }
}
