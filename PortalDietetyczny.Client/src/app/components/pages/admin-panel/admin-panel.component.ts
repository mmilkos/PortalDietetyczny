import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { RoutesEnum } from '../../../enums/RoutesEnum';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent implements OnInit
{
  login: string = RoutesEnum.login
  constructor(private accountService: AccountService, private router: Router)
  {
  }

  ngOnInit(): void
  {
    this.accountService.isLogedIn().pipe(
      catchError(error => {
        console.log(error)
        if (error.status === 401) {


          this.router.navigate([this.login])
        }
        return of(error);
      })
    ).subscribe();
  }
}
