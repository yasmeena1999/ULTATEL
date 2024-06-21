import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { RegisterComponent } from './components/register/register.component';
import { LogOutComponent } from './components/log-out/log-out.component';
import { HomeComponent } from './components/Students/home/home.component';


export const routes: Routes = [
    { path: "", redirectTo: "login/Signin", pathMatch: "full" },
  
    {path: "login", component: SignInComponent},
      { path: "", component: SignInComponent },
      { path: "Register", component: RegisterComponent },
      {path:"home",component:HomeComponent},
     
  { path: "logout", component: LogOutComponent },
];
