import { Component, Input } from '@angular/core';
import { RecipePreviewDto } from '../../../models/RecipePreviewDto';
import { Router } from '@angular/router';


@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrl: './recipe-card.component.css'
})
export class RecipeCardComponent
{
  @Input() info: RecipePreviewDto;

  constructor(private router: Router) {
  }

  navigateToRecipe()
  {
    let link : string = this.router.url + "/" + this.info.url;
    this.router.navigateByUrl(link)

  }
}
