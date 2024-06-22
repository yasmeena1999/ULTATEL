import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import AccountService from '../Services/AccountService/account.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
   
  const authToken = localStorage.getItem('token');
  if (authToken) {
    const authReq = req.clone({
      setHeaders: { Authorization: `Bearer ${authToken}` }
    });
    return next(authReq);
  }
  return next(req);
};
