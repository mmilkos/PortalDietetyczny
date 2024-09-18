import { Component, Input } from '@angular/core';
import { RecipesService } from '../../../services/recipes.service';
import {FormControl, FormGroup } from '@angular/forms';
import {AddTagDto, TagContext} from '../../../DTOs/AddTagDto';

@Component({
  selector: 'app-add-tag-form',
  templateUrl: './add-tag-form.component.html',
  styleUrl: './add-tag-form.component.css'
})
export class AddTagFormComponent
{
  @Input() tagContext: TagContext;
  constructor(private recipesService: RecipesService) {}

  addIngredients = new FormGroup(
    {
      name: new FormControl('')
    })

  add()
  {
    var ingredient: AddTagDto =
      {
        Name: this.addIngredients.get("name").value,
        context: this.tagContext
      }


    console.log(ingredient)

    this.recipesService.addTag(ingredient).subscribe(
      (response)=> this.addIngredients.reset(),
      (error) => console.log(error)
    )
  }
}
