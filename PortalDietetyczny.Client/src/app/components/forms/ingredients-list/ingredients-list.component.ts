import { Component } from '@angular/core';
import {FormControl, FormGroup } from '@angular/forms';
import { AddIngredientDto } from '../../../DTOs/AddIngredientDto';
import { RecipesService } from '../../../services/recipes.service';
import { tap } from 'rxjs';

@Component({
  selector: 'app-ingredients-list',
  templateUrl: './ingredients-list.component.html',
  styleUrl: './ingredients-list.component.css'
})
export class IngredientsListComponent
{
  constructor(private recipesService: RecipesService) {}

  addIngredients = new FormGroup(
    {
      name: new FormControl('')
    })

  add()
  {
    var ingredient: AddIngredientDto =
      {
        Name: this.addIngredients.get("name").value
      }

      console.log(ingredient)

      this.recipesService.addIngredient(ingredient).subscribe(
        (response)=> this.addIngredients.reset(),
        (error) => console.log(error)
      )

  }

}
