import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from "../../../core/services/account-service";
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private accountService = inject(AccountService)
  protected creds: any={}
  login() {
    this.accountService.login(this.creds).subscribe({
      next: results => console.log(results),
      error: error => alert(error.message)
    })
  }
}
