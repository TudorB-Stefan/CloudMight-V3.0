import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from "../../../core/services/account-service";
import { Router } from "@angular/router";
import { AuthService } from "../../../core/services/auth-service";
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private auth = inject(AuthService);
  private router = inject(Router);

  creds = { email: '', password: ''};
  errorMsg = '';
  login(){
    this.auth.login(this.creds).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err) => this.errorMsg = 'Invalid email or password'
    });
  }
}
