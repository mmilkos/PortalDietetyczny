import { Component, OnInit } from '@angular/core';
import { RecipesService } from '../../../services/recipes.service';
import { RecipePreviewDto } from '../../../models/RecipePreviewDto';
import { PagedResult } from '../../../models/PagedResult';
import { Router } from '@angular/router';
import { IdAndName } from '../../../DTOs/IdAndName';
import {RecipesPreviewPageRequest} from "../../../DTOs/RecipesPreviewPageRequest";

@Component({
  selector: 'app-recipes-grid',
  templateUrl: './recipes-grid.component.html',
  styleUrl: './recipes-grid.component.css'
})
export class RecipesGridComponent implements OnInit
{
  recipes: RecipePreviewDto[]
  tagsNames: IdAndName[] = [];
  ingredientNames: IdAndName[] = [];

  selectedTagsIds : number[] = []
  selectedIngredientsIds : number[] = []

  tagSelections = new Map<number, boolean>();
  ingredientSelections = new Map<number, boolean>();

  currentPage: number = 1;
  totalPages: number

  constructor(private recipesService: RecipesService) {}

  ngOnInit(): void
  {
    var initDto: RecipesPreviewPageRequest =
      {
        PageNumber: this.currentPage,
        PageSize: 6,
        TagsIds: [],
        IngredientsIds: []
      }

      this.getRecipes(initDto)

    this.getTagsNames()
    this.getIngredientsNames()
  }

  getRecipes(dto: RecipesPreviewPageRequest)
  {
    this.recipesService.getRecipesPaged(dto).subscribe(
      (response: PagedResult<RecipePreviewDto>)=>
      {
        this.recipes = response.data
        this.totalPages = Math.ceil(response.totalCount / 6)
      } ,
      (error) => console.log(error))
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

  toggleTagSelection(tagId: number, event: Event) {
    const inputElement = event.target as HTMLInputElement
    if (inputElement.checked) {
      this.tagSelections.set(tagId, true);
    } else {
      this.tagSelections.delete(tagId);
    }
  }

  toggleIngredientsSelection(ingredientId: number, event: Event) {
    const inputElement = event.target as HTMLInputElement
    if (inputElement.checked) {
      this.ingredientSelections.set(ingredientId, true);
    } else {
      this.ingredientSelections.delete(ingredientId);
    }
  }

  search()
  {
    var initDto: RecipesPreviewPageRequest =
      {
        PageNumber: 1,
        PageSize: 6,
        TagsIds: Array.from(this.tagSelections.keys()),
        IngredientsIds: Array.from(this.ingredientSelections.keys()),
      };

    this.getRecipes(initDto);
  }

  nextPage()
  {
    if (this.currentPage + 1 <= this.totalPages) {
      this.currentPage++;
    }

    this.getRecipes({
      PageNumber: this.currentPage,
      PageSize: 6,
      TagsIds: Array.from(this.tagSelections.keys()),
      IngredientsIds: Array.from(this.ingredientSelections.keys()),
    });

    window.scrollTo(0, 0);
  }

  previousPage()
  {
    this.currentPage--;
    if (this.currentPage < 1) this.currentPage = 1


    this.getRecipes({
      PageNumber: this.currentPage,
      PageSize: 6,
      TagsIds: Array.from(this.tagSelections.keys()),
      IngredientsIds: Array.from(this.ingredientSelections.keys()),
    });

    window.scrollTo(0, 0);
  }
}
