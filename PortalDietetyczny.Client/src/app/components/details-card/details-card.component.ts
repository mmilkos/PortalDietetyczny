import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-details-card',
  templateUrl: './details-card.component.html',
  styleUrl: './details-card.component.css'
})
export class DetailsCardComponent
{
  @Input() title: string | undefined;
  @Input() content: string | undefined;
  @Input() iconClass: string | undefined;

}

