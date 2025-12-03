import { Component, inject } from '@angular/core';
import {AuthService } from "../../../core/services/auth-service";
import { Router } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-register',
  imports: [FormsModule,CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private auth = inject(AuthService);
  private router = inject(Router);
  creds = {
    userName: '',
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: ''

  };
  errorMsg = '';
  register(){
    this.auth.register(this.creds).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: (err) => this.errorMsg = 'Registration failed'
    })
  }
}
