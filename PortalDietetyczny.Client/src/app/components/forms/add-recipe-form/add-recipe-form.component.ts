import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { RecipesService } from '../../../services/recipes.service';
import { RecipeIngredientDto } from '../../../DTOs/RecipeIngredientDto';
import { IdAndName } from '../../../DTOs/IdAndName';
@Component({
  selector: 'app-add-recipe-form',
  templateUrl: './add-recipe-form.component.html',
  styleUrl: './add-recipe-form.component.css'
})
export class AddRecipeFormComponent
{
  recipeForm: FormGroup;
  ingredientForm: FormGroup;
  tagsForm: FormGroup;
  tagsNames: IdAndName[] = [];
  selectedIngredientsNames: string[] = []; //wyswietlane nazwy po dodaniu
  ingredientNames: IdAndName[] = []; //pobrane nazwy wszystkich składników
  addedIngredients: RecipeIngredientDto[] = []; // lista skladnikow ktora wysylamy na backend
  steps: string[];
  newStep: string= "";

  selectedTags: string[] = []


  constructor(private fb: FormBuilder, private recipesService: RecipesService)
  {
    this.getIngredientsNames()
    this.getTagsNames()
  }


  ngOnInit() {
    this.ingredientForm = this.fb.group({
      ingredientName: ['', Validators.required],
      weightValue: [''],
      weightUnit: [''],
      homeValue: [''],
      homeUnit: ['']
    });

    this.tagsForm = this.fb.group(
      {
        tagName: ['', Validators.required]

      })

    this.recipeForm = this.fb.group({
      recipeName: [''],
      instruction: [''],
      tags: [''],
      kcal: [''],
      fat: [''],
      carb: [''],
      protein: [''],
      fiber: ['']
    });
  }

  addIngredient()
  {
    const ingredient = this.ingredientForm.value;
    console.log(ingredient)
    let displayName = ingredient.ingredientName.name

    let recipeIngredient: RecipeIngredientDto =
      {
        ingredientId: ingredient.ingredientName.id,
        ingredientName: ingredient.ingredientName.name,
        unit: ingredient.weightUnit,
        unitValue: ingredient.weightValue,
        homeUnit: ingredient.homeUnit,
        homeUnitValue: ingredient.homeValue
      }

    this.addedIngredients.push(recipeIngredient)
    this.selectedIngredientsNames.push(displayName);
    this.ingredientForm.reset();
  }

  removeIngredient(ingredient : any) {
    this.selectedIngredientsNames = this.selectedIngredientsNames.filter(i => i !== ingredient);
    this.addedIngredients = this.addedIngredients.filter(i => i.ingredientName !== ingredient)
  }

  getIngredientsNames()
  {
    this.recipesService.getIngredients().subscribe(
      dto =>
      {
        this.ingredientNames = dto.ingredients;
        console.log(this.ingredientNames)
      },
      error => console.log(error)
    )
  }

  removeStep(index: number) {
    this.steps.splice(index, 1);
  }

  addStep() {
    console.log(this.newStep)
      this.steps.push(this.newStep);
      this.newStep = ''; // clear the input

    console.log(this.steps)
  }

  getTagsNames()
  {
    this.recipesService.getTags().subscribe(
      dto =>
      {
        this.tagsNames = dto.tags;
      },
      error => console.log(error)
    )
  }

  addTag() {
    const tag = this.tagsForm.value;
    let tagName = tag.tagName;

    this.selectedTags.push(tagName.name);
    this.tagsForm.reset();
  }

  removeTag(tag: string) {
    this.selectedTags = this.selectedTags.filter(t => t !== tag);
  }

  submitRecipe() {
    console.log(this.recipeForm.value);
    console.log(this.addedIngredients);

  }
}
