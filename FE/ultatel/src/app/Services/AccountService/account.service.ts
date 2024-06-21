import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import {  Observable } from 'rxjs';
import * as jwtDecode from 'jwt-decode';

import { LoginDto } from '../../Models/LoginDto';
import { RegisterDto } from '../../Models/RegisterDto';

@Injectable({
  providedIn: 'root'
})
export default class AccountService {
  
  private apiUrl = 'https://4f39-102-44-174-195.ngrok-free.app/api/account'; // Replace with your API URL
  
  constructor(
    private http: HttpClient,
    private router: Router,
    
  ) {
   
  }

  login(loginDto: LoginDto):  Promise<boolean> {
    let str: string = `/Login`;
  
    return new Promise<boolean>((resolve) => {
      this.http.post(this.apiUrl + str, loginDto, { responseType: 'text' }).subscribe(
        (response) => {
          
       
          
          localStorage.setItem("token", response);
       
          resolve(true); 
        },
        (error) => {
          console.error("Error occurred during login:", error);
          resolve(false); 
        }
      );
    });
  }
  

  register(registerDto: RegisterDto): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerDto);
  }

  logout(): void {
    localStorage.removeItem('token');
   
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    const token = localStorage.getItem('token');
    console.log('Retrieved Token:', token); // Debugging: Log the retrieved token
    return token;
  }
  

  
  getNameIdFromToken(): string | null {
    const token = this.getToken();
    if (token) {
      try {
        const decodedToken: any = jwtDecode.jwtDecode(token);
        console.log(decodedToken);
        return decodedToken.nameid|| null;
      } catch (error) {
        console.error('Error decoding token:', error);
        return "error decoding";
      }
    }
    return "no token";
  }
}