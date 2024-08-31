import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipePreviewDto } from '../models/RecipePreviewDto';
import { PagedResult } from '../models/PagedResult';
import { AddIngredientDto } from '../DTOs/AddIngredientDto';
import { AddTagDto } from '../DTOs/AddTagDto';
import {IngredientListDto, TagListDto} from '../DTOs/IdAndName';
import { AddRecipeDto } from '../DTOs/AddRecipeDto';
import { RecipesPreviewPageRequest } from '../DTOs/RecipesPreviewPageRequest';

@Injectable({
  providedIn: 'root'
})
export class RecipesService
{
  apiUrl: string = "https://localhost:44317/api/recipes"

  constructor(private http: HttpClient) { }

  addRecipe(recipe: AddRecipeDto): Observable<any>
  {
    console.log(recipe)
    return this.http.post<any>(this.apiUrl, recipe);
  }

  getRecipesPaged(params: RecipesPreviewPageRequest) : Observable<PagedResult<RecipePreviewDto>>
  {
    console.log(params);
    return this.http.post<PagedResult<RecipePreviewDto>>(this.apiUrl + "/paged", params)
  }

  addIngredient(recipe: AddIngredientDto): Observable<any>
  {
    console.log(recipe)
    return this.http.post<any>(this.apiUrl + '/ingredients', recipe);
  }

  addTag(tag: AddTagDto): Observable<any>
  {
    console.log(tag)
    return this.http.post<any>(this.apiUrl + '/tags', tag);
  }

  getTags(): Observable<TagListDto>
  {
    return this.http.get<TagListDto>(this.apiUrl + '/tags');
  }

  getIngredients() : Observable<IngredientListDto>
  {
    return this.http.get<IngredientListDto>(this.apiUrl + '/ingredients');
  }


}
