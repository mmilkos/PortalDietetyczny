import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipePreviewDto } from '../models/RecipePreviewDto';
import { PagedResult } from '../models/PagedResult';
import { AddIngredientDto } from '../DTOs/AddIngredientDto';
import { AddTagDto } from '../DTOs/AddTagDto';
import {IngredientListDto, TagListDto} from '../DTOs/IdAndName';

@Injectable({
  providedIn: 'root'
})
export class RecipesService
{
  apiUrl: string = "https://localhost:44317/api/recipes"

  constructor(private http: HttpClient) { }

  getRecipesPaged() : Observable<PagedResult<RecipePreviewDto>>
  {
    return this.http.get<PagedResult<RecipePreviewDto>>(this.apiUrl)
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
