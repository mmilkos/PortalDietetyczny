import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RoutesEnum } from '../../../enums/RoutesEnum';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent
{
  public routes  = RoutesEnum;
  constructor(private router: Router) {
  }
  navigateTo(url : string)
  {
    this.router.navigate([url])
  }
  openLinkedin() {
    window.open('https://www.linkedin.com/in/mateusz-milkowski2002/', '_blank');
  }
}
