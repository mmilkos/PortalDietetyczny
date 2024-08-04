import { ViewportScroller } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {Router, RouterLink } from '@angular/router';
import { RoutesEnum } from '../../../enums/RoutesEnum';

@Component({
  selector: 'app-details-grid',
  templateUrl: './details-grid.component.html',
  styleUrl: './details-grid.component.css'
})
export class DetailsGridComponent implements OnInit
{
  public isSmallScreen: boolean = false;
  public routes  = RoutesEnum;

  constructor(private router: Router) {
  }

  ngOnInit(): void
  {
    window.addEventListener('resize', () => {
      this.isSmallScreen = window.innerWidth <= 470;
    });
  }

  navigateTo(url : string)
  {
    this.router.navigate([url])
  }

}
