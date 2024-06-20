import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginDto } from '../../Models/LoginDto';
import { RegisterDto } from '../../Models/RegisterDto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private apiUrl = '"http://localhost:4200/api/account"';

  constructor(private http: HttpClient) {}

  login(credentials: LoginDto): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials);
  }

  register(user: RegisterDto): Observable<{ status: string; message: string }> {
    return this.http.post<{ status: string; message: string }>(`${this.apiUrl}/register`, user);
  }

  saveToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  logout(): void {
    localStorage.removeItem('authToken');
  }
}