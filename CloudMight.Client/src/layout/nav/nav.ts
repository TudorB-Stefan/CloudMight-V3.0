import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from "../../core/services/auth-service";

@Component({
  selector: 'app-nav',
  imports: [],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  auth = inject(AuthService);
  logout(){
    this.auth.logout();
  }
  ngOnInit(){
    this.auth.isLoggedIn();
  }
}
