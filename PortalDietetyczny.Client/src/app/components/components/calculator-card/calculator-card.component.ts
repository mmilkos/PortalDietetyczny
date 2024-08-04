import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-calculator-card',
  templateUrl: './calculator-card.component.html',
  styleUrl: './calculator-card.component.css'
})
export class CalculatorCardComponent
{
  @Input() header: string = '';
  @Input() description: string[] = [];
  @Input() isSmallScreen: boolean = false;
  @Input() isGreyBackground: boolean = false;

}
