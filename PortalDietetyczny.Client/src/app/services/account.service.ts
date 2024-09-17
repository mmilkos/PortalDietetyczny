import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService
{
  apiUrl: string = "https://localhost:44317/api/account"

  constructor(private http: HttpClient) { }

  startApp(): Observable<any>
  {
    return this.http.post(this.apiUrl + "/startApp", null);
  }
}
