import { Component } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import {FormControl, FormGroup } from '@angular/forms';
import { LoginUserRequestDto } from '../../../DTOs/LoginUserRequestDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent
{
  constructor(private accountService: AccountService)
  {

  }

  loginForm = new FormGroup({
    login: new FormControl(''),
    password: new FormControl('')
  });

  login()
  {
    var dto : LoginUserRequestDto =
      {
        username: this.loginForm.get("login").value,
        password: this.loginForm.get("password").value
      }

      this.accountService.login(dto).subscribe(
        ()=> console.log("Logged in"),
        (error)=> console.log(error.error)
      )

    this.loginForm.reset()
  }

}
