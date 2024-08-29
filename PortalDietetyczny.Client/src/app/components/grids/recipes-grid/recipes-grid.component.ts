import { Component, OnInit } from '@angular/core';
import { RecipesService } from '../../../services/recipes.service';
import { RecipePreviewDto } from '../../../models/RecipePreviewDto';
import { PagedResult } from '../../../models/PagedResult';

@Component({
  selector: 'app-recipes-grid',
  templateUrl: './recipes-grid.component.html',
  styleUrl: './recipes-grid.component.css'
})
export class RecipesGridComponent implements OnInit
{
  recipes: RecipePreviewDto[]
  constructor(private recipesService: RecipesService)
  {

  }

  ngOnInit(): void
  {
    this.recipesService.getRecipesPaged().subscribe(
      (response: PagedResult<RecipePreviewDto>)=>
      {
        this.recipes = response.data
        console.log(response)
      } ,
      (error) => console.log(error))
  }

}
