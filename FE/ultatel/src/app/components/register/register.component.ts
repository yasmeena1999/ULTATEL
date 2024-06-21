import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import AccountService from '../../Services/AccountService/account.service';
import { RegisterDto } from '../../Models/RegisterDto';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const passwordControl = form.get('password');
    const confirmPasswordControl = form.get('confirmPassword');
    
    if (!passwordControl || !confirmPasswordControl) {
      return null;
    }

    return passwordControl.value === confirmPasswordControl.value ? null : { mismatch: true };
  }

  register(): void {
    if (this.registerForm.valid) {
      const registerDto: RegisterDto = this.registerForm.value;
      this.accountService.register(registerDto).subscribe(
        response => {
          Swal.fire({
            icon: 'success',
            title: 'Registration Successful',
            text: 'You have successfully registered.',
            showConfirmButton: false,
            timer: 1500
          }).then(() => {
            this.router.navigate(['/login']);
          });
        },
        error => {
          Swal.fire({
            icon: 'error',
            title: 'Registration Failed',
            text: 'Failed to register. Please try again later.',
            showConfirmButton: true
          });
          console.error('Registration failed', error);
        }
      );
    } else {
      // Form is invalid, show validation errors with SweetAlert
      this.showValidationAlert();
    }
  }

  showValidationAlert() {
    let validationMessage = 'Please fill out all required fields correctly.';
    if (this.registerForm.get('email')?.hasError('email')) {
      validationMessage = 'Please enter a valid email address.';
    } else if (this.registerForm.hasError('mismatch')) {
      validationMessage = 'Passwords do not match.';
    }
    
    Swal.fire({
      icon: 'error',
      title: 'Validation Error',
      text: validationMessage,
      showConfirmButton: true
    });
  }
}