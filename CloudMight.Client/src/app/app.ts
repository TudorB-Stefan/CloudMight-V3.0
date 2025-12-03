import { HttpClient } from "@angular/common/http";
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Nav } from "./layout/nav/nav";

@Component({
  selector: 'app-root',
  imports: [Nav,RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  private http = inject(HttpClient);
  protected readonly title = signal('CloudMight.Client');
  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/auth/').subscribe(
      {
        next:response=>console.log(response),
        error:error=>console.log(error),
        complete: () => console.log('Completed the http request')
      })
  }
}
