import { Component, OnInit } from '@angular/core';
import { RoutesEnum } from '../../enums/RoutesEnum';
import { ViewportScroller } from '@angular/common';

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

  constructor(private viewportScroller: ViewportScroller) { }

  ngOnInit(): void
  {
    window.addEventListener('resize', () => {
      console.log(window.innerWidth);

      this.isBigScreen = window.innerWidth <= 431;
    });
  }
  openInstagram() {
    window.open('https://www.instagram.com/portaldietetyczny/', '_blank');
  }

  openFacebook() {
    window.open('https://www.facebook.com/gotowejadlospisy/', '_blank');
  }
}
