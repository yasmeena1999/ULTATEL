import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import AccountService from '../../Services/AccountService/account.service';
import { Router } from '@angular/router';
import { LoginDto } from '../../Models/LoginDto';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  loginForm: FormGroup; // Explicitly define loginForm as FormGroup

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
    if (this.loginForm.invalid) {
      this.showValidationErrors();
      return;
    }

    const loginDto: LoginDto = {
      email: this.loginForm.get('email')?.value,
      password: this.loginForm.get('password')?.value
    };

    this.accountService.login(loginDto).then(success => {
      if (success) {
        Swal.fire({
          icon: 'success',
          title: 'Login Successful',
          text: 'You have successfully logged in!',
        }).then(() => {
          this.router.navigate(['/home']);
        });
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Invalid Data',
          text: 'Please enter valid data.',
        });
      }
    });
  }

  showValidationErrors(): void {
    if (this.loginForm.get('email')?.hasError('required')) {
      Swal.fire({
        icon: 'error',
        title: 'Email is required',
        text: 'Please enter your email address.',
      });
    } else if (this.loginForm.get('email')?.hasError('email')) {
      Swal.fire({
        icon: 'error',
        title: 'Invalid Email',
        text: 'Please enter a valid email address.',
      });
    } else if (this.loginForm.get('password')?.hasError('required')) {
      Swal.fire({
        icon: 'error',
        title: 'Password is required',
        text: 'Please enter your password.',
      });
    }
  }

  goToRegister(): void {
    this.router.navigate(['/Register']);
  }
}