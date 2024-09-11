import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-consultation-card',
  templateUrl: './consultation-card.component.html',
  styleUrl: './consultation-card.component.css'
})
export class ConsultationCardComponent
{
  @Input() img: string;
  @Input() name: string;

}
