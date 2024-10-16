import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipePreviewDto } from '../models/RecipePreviewDto';
import { PagedResult } from '../models/PagedResult';
import { AddIngredientDto } from '../DTOs/AddIngredientDto';
import { AddTagDto } from '../DTOs/AddTagDto';
import { NamesListDto} from '../DTOs/IdAndName';
import { AddRecipeDto } from '../DTOs/AddRecipeDto';
import { RecipesPreviewPageRequest } from '../DTOs/RecipesPreviewPageRequest';
import { RecipeDetailsDto } from '../DTOs/RecipeDetailsDto';
import { config } from '../config';

@Injectable({
  providedIn: 'root'
})
export class RecipesService
{
  apiUrl: string = config.API_URL + "recipes"

  constructor(private http: HttpClient) { }

  addRecipe(recipe: AddRecipeDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl, recipe, {withCredentials: true});
  }

  getRecipesPaged(params: RecipesPreviewPageRequest) : Observable<PagedResult<RecipePreviewDto>>
  {
    return this.http.post<PagedResult<RecipePreviewDto>>(this.apiUrl + "/paged", params)
  }

  getRecipes(): Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl);
  }

  getRecipe(link: string): Observable<RecipeDetailsDto>
  {
    return this.http.get<RecipeDetailsDto>(this.apiUrl + `/${link}`)
  }

  deleteRecipe(id: number): Observable<any>
  {
    return this.http.delete(this.apiUrl + `/${id}`, {withCredentials: true})
  }

  getIngredients() : Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl + '/ingredients');
  }

  addIngredient(recipe: AddIngredientDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl + '/ingredients', recipe, {withCredentials: true});
  }

  deleteIngredient(id: number): Observable<any>
  {
    return this.http.delete(this.apiUrl + `/ingredients/${id}`, {withCredentials: true})
  }

  addTag(tag: AddTagDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl + '/tags', tag, {withCredentials: true});
  }

  getTags(): Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl + '/tags');
  }

  deleteTag(id: number): Observable<any>
  {
    return this.http.delete(this.apiUrl + `/tags/${id}`, {withCredentials: true})
  }

}
