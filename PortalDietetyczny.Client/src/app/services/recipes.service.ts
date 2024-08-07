import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipePreviewDto } from '../models/RecipePreviewDto';
import { PagedResult } from '../models/PagedResult';

@Injectable({
  providedIn: 'root'
})
export class RecipesService
{
  apiUrl: string = "https://localhost:44317/api/recipes"

  constructor(private http: HttpClient) { }

  GetRecipesPaged() : Observable<PagedResult<RecipePreviewDto>>
  {
    return this.http.get<PagedResult<RecipePreviewDto>>(this.apiUrl)
  }
}
