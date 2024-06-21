import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // Check if token exists
    const token = localStorage.getItem('token');
    if (token !== null && token !== undefined && token !== '') {
      // User is authenticated
      if (state.url.includes('login') || state.url.includes('register')) {
        // If trying to access login or register page while authenticated, redirect to home
        this.router.navigate(['/home']);
        return false;
      }
      // Allow access to the requested route
      return true;
    } else {
      // User is not authenticated
      if (state.url.includes('home')) {
        // If trying to access home page without authentication, redirect to login
        this.router.navigate(['/login']);
        return false;
      }
      // Allow access to non-protected routes like login and register when not authenticated
      return true;
    }
  }
}
