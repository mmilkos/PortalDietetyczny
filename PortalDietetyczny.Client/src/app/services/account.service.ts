import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginUserRequestDto } from '../DTOs/LoginUserRequestDto';
import { JwtTokenDto } from '../DTOs/JwtTokenDto';
import { config } from '../config';

@Injectable({
  providedIn: 'root'
})
export class AccountService
{
  apiUrl: string = config.API_URL + "account"

  constructor(private http: HttpClient) { }

  login(dto : LoginUserRequestDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl + "/login", dto, { withCredentials: true })
  }

  logout() : Observable<any>
  {
    return this.http.post(this.apiUrl + "/logout", null,{ withCredentials: true } )
  }

  isLogedIn(): Observable<any>
  {
    return this.http.post(this.apiUrl,null , { withCredentials: true })
  }

}
