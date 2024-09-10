import { Component, Input, OnInit } from '@angular/core';
import { RecipeDetailsDto } from '../../../DTOs/RecipeDetailsDto';
import { RecipesService } from '../../../services/recipes.service';
import { Router } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrl: './recipe-details.component.css'
})
export class RecipeDetailsComponent implements OnInit
{
  recipeDetails: RecipeDetailsDto;
  uid: number

  constructor(private recipesService: RecipesService, private router: Router)
  {
    let link = this.router.url.split("-")
    this.uid = parseInt(link[link.length - 1], 16)
  }

  ngOnInit(): void
  {
    this.recipesService.getRecipeDetails(this.uid).subscribe(
      (response) =>
      {
        this.recipeDetails = response
        console.log(response)
      },
      (error) => console.log(error.error)
    );
    console.log("test: " + this.recipeDetails)
  }

  previousPage()
  {
    this.router.navigate(['/przepisy']);
  }
}
