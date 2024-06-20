import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../Services/AccountService/account.service';
import { Router } from '@angular/router';
import { LoginDto } from '../../Models/LoginDto';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  loginForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  login(): void {
    if (this.loginForm.valid) {
      const loginDto: LoginDto = this.loginForm.value;
      this.accountService.login(loginDto).subscribe(
        response => {
          console.log('Login successful', response);
          this.accountService.saveToken(response.token);
          this.router.navigate(['/dashboard']); // Adjust the route as needed
        },
        error => {
          console.error('Login failed', error);
        }
      );
    }
  }
}
