import { Component, Input } from '@angular/core';
import { RecipePreviewDto } from '../../../models/RecipePreviewDto';


@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrl: './recipe-card.component.css'
})
export class RecipeCardComponent
{
  @Input() info: RecipePreviewDto;
}
