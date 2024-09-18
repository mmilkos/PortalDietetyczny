import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginUserRequestDto } from '../DTOs/LoginUserRequestDto';
import { JwtTokenDto } from '../DTOs/JwtTokenDto';

@Injectable({
  providedIn: 'root'
})
export class AccountService
{
  apiUrl: string = "https://localhost:44317/api/account"

  constructor(private http: HttpClient) { }

  login(dto : LoginUserRequestDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl + "/login", dto, { withCredentials: true })
  }

  isLogedIn(): Observable<any>
  {

    return this.http.post(this.apiUrl,null , { withCredentials: true })
  }

}
