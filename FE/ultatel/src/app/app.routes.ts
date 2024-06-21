import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { RegisterComponent } from './components/register/register.component';
import { LogOutComponent } from './components/log-out/log-out.component';
import { HomeComponent } from './components/Students/home/home.component';
import { AuthGuard } from './Guards/auth.guard';


export const routes: Routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
  
    {path: "login", component: SignInComponent,canActivate:[AuthGuard]},
     
      { path: "Register", component: RegisterComponent ,canActivate:[AuthGuard]},
      {path:"home",component:HomeComponent,canActivate:[AuthGuard]},
     
  { path: "logout", component: LogOutComponent },
];
