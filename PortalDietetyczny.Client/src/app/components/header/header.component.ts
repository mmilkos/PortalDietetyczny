import { Component } from '@angular/core';
import { RoutesEnum } from '../../enums/RoutesEnum';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent
{
  public routes = RoutesEnum;
  logo = 'assets/logo.png';

}
