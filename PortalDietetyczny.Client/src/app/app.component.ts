import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'PortalDietetyczny.Client';

  constructor(private accountService : AccountService) {}

  ngOnInit(): void
  {
    this.accountService.startApp().subscribe(
      ()=>
      {
        console.log("Application has started")
      },
      error => console.log(error.error))
  }
}
