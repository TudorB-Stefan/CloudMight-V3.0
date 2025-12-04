import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from "../../core/services/auth-service";

@Component({
  selector: 'app-nav',
  imports: [],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  protected authService = inject(AuthService);
  logout(){
    this.authService.logout();
  }
  ngOnInit(){
    this.authService.isLoggedIn();
  }
}
