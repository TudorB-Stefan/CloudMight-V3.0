import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from "../../core/services/auth-service";

@Component({
  selector: 'app-nav',
  imports: [],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  private auth = inject(AuthService);
  ngOnInit(){
    this.auth.isLoggedin();
  }
}
