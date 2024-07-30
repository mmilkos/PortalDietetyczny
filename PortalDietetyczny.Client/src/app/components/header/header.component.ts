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
  public viewportWidth: number = 0;
  public isSmallScreen: boolean = false;

  constructor(private viewportScroller: ViewportScroller) { }

  ngOnInit(): void
  {
    this.viewportWidth = this.viewportScroller.getScrollPosition()[0];
    window.onresize = () => {
      this.viewportWidth = window.innerWidth;
      this.isSmallScreen = window.innerWidth <= 431
      console.log("Czy jest mały erkan; " + this.isSmallScreen)
      console.log("Wielkosc erkanu " + this.viewportWidth)
    }
  }
}
