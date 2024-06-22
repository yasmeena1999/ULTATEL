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
    
    const token = localStorage.getItem('token');
    if (token !== null && token !== undefined && token !== '') {
    
      if (state.url.includes('login') || state.url.includes('register')) { this.router.navigate(['/home']);
        return false;
      }
      
      return true;
    } else {
     
      if (state.url.includes('home')) {
        
        this.router.navigate(['/login']);
        return false;
      }
   return true;
    }
  }
}
