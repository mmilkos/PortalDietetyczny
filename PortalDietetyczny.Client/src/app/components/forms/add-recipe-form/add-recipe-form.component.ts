import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { RecipesService } from '../../../services/recipes.service';
import { IdAndName } from '../../../DTOs/IdAndName';
import {FileItem, FileUploader } from 'ng2-file-upload';
import { AddRecipeDto, NutritionInfo, RecipeIngredientDto } from '../../../DTOs/AddRecipeDto';
import { TagContext } from '../../../DTOs/AddTagDto';
@Component({
  selector: 'app-add-recipe-form',
  templateUrl: './add-recipe-form.component.html',
  styleUrl: './add-recipe-form.component.css'
})
export class AddRecipeFormComponent
{
  tagContextRecipe: TagContext = TagContext.Recipe;
  recipeForm: FormGroup;
  ingredientForm: FormGroup;
  tagsForm: FormGroup;
  tagsNames: IdAndName[] = [];
  selectedIngredientsNames: string[] = []; //wyswietlane nazwy po dodaniu
  ingredientNames: IdAndName[] = []; //pobrane nazwy wszystkich składników
  addedIngredients: RecipeIngredientDto[] = []; // lista skladnikow ktora wysylamy na backend
  steps: string[];
  newStep: string= "";
  fileName: string = "";
  selectedTagToDelete: number;
  selectedIngredientToDelete: number;
  selectedRecipeToDelete: number;
  recipeNames : IdAndName[] = []

  photo: string;

  selectedTags: string[] = []
  selectedTagsIds: number[] = []


  constructor(private fb: FormBuilder, private recipesService: RecipesService)
  {
    this.getIngredientsNames()
    this.getTagsNames()
    this.getRecipeNames()
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
    let displayName = ingredient.ingredientName.name

    let recipeIngredient: RecipeIngredientDto =
      {
        id: ingredient.ingredientName.id,
        name: ingredient.ingredientName.name,
        unit: ingredient.weightUnit,
        unitValue: ingredient.weightValue,
        homeUnit: ingredient.homeUnit,
        homeUnitValue: ingredient.homeValue
      }

    this.addedIngredients.push(recipeIngredient)
    this.selectedIngredientsNames.push(displayName);
    this.ingredientForm.reset();
  }

  deleteIngredient()
  {
    this.recipesService.deleteIngredient(this.selectedIngredientToDelete).subscribe(
      (response)=>{},
      (error) => console.log(error.error))
  }

  removeIngredient(ingredient : any) {
    this.selectedIngredientsNames = this.selectedIngredientsNames.filter(i => i !== ingredient);
    this.addedIngredients = this.addedIngredients.filter(i => i.name !== ingredient)
  }

  getIngredientsNames()
  {
    this.recipesService.getIngredients().subscribe(
      dto =>
      {
        this.ingredientNames = dto.names;
      },
      error => console.log(error)
    )
  }

  removeStep(index: number) {
    this.steps.splice(index, 1);
  }

  addStep() {
      this.steps.push(this.newStep);
      this.newStep = ''; // clear the input
  }

  getTagsNames()
  {
    this.recipesService.getTags().subscribe(
      dto  =>
      {
        this.tagsNames = dto.names;
      },
      error => console.log(error)
    )
  }

  addTag() {
    const tag = this.tagsForm.value;
    let tagName = tag.tagName.name;
    let tagId = tag.tagName.id;

    this.selectedTags.push(tagName);
    this.selectedTagsIds.push(tagId)
    this.tagsForm.reset();
  }

  deleteTag()
  {
    this.recipesService.deleteTag(this.selectedTagToDelete).subscribe(
      (response)=>{},
      (error) => console.log(error.error))
  }

  removeTag(tag: string) {
    this.selectedTags = this.selectedTags.filter(t => t !== tag);
  }

  getRecipeNames()
  {
    this.recipesService.getRecipes().subscribe(
      dto  =>
      {
        this.recipeNames = dto.names;
        console.log(this.recipeNames)
      },
      error => console.log(error)
    )
  }

  deleteRecipe()
  {
    this.recipesService.deleteRecipe(this.selectedRecipeToDelete).subscribe(
      (response)=>{},
      (error) => console.log(error.error))
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files[0];

    const reader = new FileReader();

    reader.onload = (e: any) =>
    {
      const arrayBuffer = e.target.result;
      const bytes = new Uint8Array(arrayBuffer);
      let binary = '';
      bytes.forEach((byte) => binary += String.fromCharCode(byte));
      this.photo = btoa(binary);
    }

    reader.readAsArrayBuffer(file);
    this.fileName = file.name
  }

  submitRecipe() {
    const recipeFormVal = this.recipeForm.value;

    const nutritionInfo: NutritionInfo = {
      fiber: recipeFormVal.fiber,
      fat: recipeFormVal.fat,
      carb: recipeFormVal.carb,
      protein: recipeFormVal.protein,
      kcal: recipeFormVal.kcal,
    };

    const addRecipeDto: AddRecipeDto = {
      tagsIds: this.selectedTagsIds,
      name: recipeFormVal.recipeName,
      ingredients: this.addedIngredients,
      nutritionInfo: nutritionInfo,
      instruction: recipeFormVal.instruction,
      fileBytes: this.photo || null,
      fileName: this.fileName
    };

    this.recipesService.addRecipe(addRecipeDto).subscribe(
      response => {},
      error => console.log(error.error)
    )
  }
}
