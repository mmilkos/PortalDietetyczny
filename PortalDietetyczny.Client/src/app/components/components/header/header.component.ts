import { Component, OnInit } from '@angular/core';
import { ViewportScroller } from '@angular/common';
import { RoutesEnum } from '../../../enums/RoutesEnum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit
{
  public routes = RoutesEnum;
  logo = 'assets/logo.png';
  public isBigScreen: boolean = false;
  public instagram = "https://www.instagram.com/portaldietetyczny/"
  public facebook = "https://www.facebook.com/gotowejadlospisy/"

  constructor(private router: Router) { }

  ngOnInit(): void
  {
    window.addEventListener('resize', () => this.isBigScreen = window.innerWidth <= 1001);
  }
  openInstagram() {
    window.open('https://www.instagram.com/portaldietetyczny/', '_blank');
  }

  openFacebook() {
    window.open('https://www.facebook.com/gotowejadlospisy/', '_blank');
  }

  goToShhoppingCart()
  {
    this.router.navigate([this.routes.shopping_cart])
  }
}
