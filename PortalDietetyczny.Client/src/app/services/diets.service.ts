import { Injectable } from '@angular/core';
import { AddDietDto } from '../DTOs/AddDietDto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NamesListDto } from '../DTOs/IdAndName';
import { AddTagDto } from '../DTOs/AddTagDto';
import { PagedResult } from '../models/PagedResult';
import { DietPreviewDto } from '../DTOs/DietPreviewDto';
import { DietsPreviewPageRequest } from '../DTOs/DietsPreviewPageRequest';
import { config } from '../config';

@Injectable({
  providedIn: 'root'
})
export class DietsService {
  apiUrl: string =  config.API_URL + "diets"

  dietsIds : number[] = []
  constructor(private http : HttpClient) { }

  addDiet(dto: AddDietDto): Observable<any>
  {
    return this.http.post(this.apiUrl, dto, {withCredentials: true});
  }

  getTags(): Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl + '/tags');
  }

  getDiets(): Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl);
  }

  addTag(tag: AddTagDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl + '/tags', tag, {withCredentials: true});
  }
  deleteTag(id: number): Observable<any>
  {
    return this.http.delete(this.apiUrl + `/tags/${id}`, {withCredentials: true})
  }

  deleteDiet(id: number): Observable<any>
  {
    return this.http.delete(this.apiUrl + `/${id}`, {withCredentials: true})
  }

  getDietsPaged(params: DietsPreviewPageRequest): Observable<PagedResult<DietPreviewDto>>
  {
    return this.http.post<PagedResult<DietPreviewDto>>(this.apiUrl + "/paged", params)
  }
}
