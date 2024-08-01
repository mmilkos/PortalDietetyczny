import { ViewportScroller } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-details-grid',
  templateUrl: './details-grid.component.html',
  styleUrl: './details-grid.component.css'
})
export class DetailsGridComponent implements OnInit
{
  public viewportWidth: number = 0;
  public isSmallScreen: boolean = true;
  constructor(private viewportScroller: ViewportScroller)
  {
    window.onresize = () => {
      this.viewportWidth = window.innerWidth;
      this.isSmallScreen = window.innerWidth <= 431
    }
    this.isSmallScreen = window.innerWidth <= 431
  }

  ngOnInit(): void
  {
    window.onresize = () => {
      this.viewportWidth = window.innerWidth;
      this.isSmallScreen = window.innerWidth <= 431
    }
    this.isSmallScreen = window.innerWidth <= 431
  }

}
