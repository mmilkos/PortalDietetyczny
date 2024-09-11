import { Component, Input, OnInit } from '@angular/core';
import { RecipeDetailsDto } from '../../../DTOs/RecipeDetailsDto';
import { RecipesService } from '../../../services/recipes.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrl: './recipe-details.component.css'
})
export class RecipeDetailsComponent implements OnInit
{
  recipeDetails: RecipeDetailsDto;
  uid: string

  constructor(private recipesService: RecipesService, private router: Router)
  {
    let link = this.router.url.split("/")
    this.uid = link[link.length - 1]
  }

  ngOnInit(): void
  {
    let link = this.router.url.split("-")
    let uidInt = parseInt(link[link.length - 1], 16);

    let json = window.sessionStorage.getItem(uidInt.toString())

    if (json == null)
    {
      this.getRecipeDetails()
    }
    else
    {
      let object : RecipeDetailsDto = JSON.parse(json)
      this.recipeDetails = object;
    }
  }

  getRecipeDetails(): void
  {
    this.recipesService.getRecipe(this.uid).subscribe(
      (response) =>
      {
        this.recipeDetails = response

        try {
          window.sessionStorage.setItem(response.uid.toString(), JSON.stringify(response));
        } catch (error) {
          window.sessionStorage.clear();
          window.sessionStorage.setItem(response.uid.toString(), JSON.stringify(response));
        }
      },
      (error) => console.log(error.error)
    );
  }

  previousPage()
  {
    this.router.navigate(['/przepisy']);
  }
}
