import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../Services/AccountService/account.service';

export class authInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = this.accountService.getToken();
    if (authToken) {
      const authReq = req.clone({
        setHeaders: { Authorization: `Bearer ${authToken}` }
      });
      return next.handle(authReq);
    }
    return next.handle(req);
  }
}
