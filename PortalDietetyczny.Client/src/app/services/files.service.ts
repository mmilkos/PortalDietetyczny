import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddFileDto } from '../DTOs/AddFileDto';
import { NamesListDto } from '../DTOs/IdAndName';

@Injectable({
  providedIn: 'root'
})
export class FilesService
{
  apiUrl: string = "https://localhost:44317/api/file"

  constructor(private http: HttpClient) { }

  addFile(file: AddFileDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl, file, {withCredentials: true});
  }

  getAllFiles(): Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl + "/downloadableFiles")
  }

  deleteFile(id: number): Observable<any>
  {
    return this.http.delete<any>(`${this.apiUrl}/${id}`, {withCredentials: true});
  }

  getDownloadableFile(id: number) : Observable<HttpResponse<Blob>> {

    return this.http.post(`${this.apiUrl}/${id}`, null,
      {
        observe: 'response',
        responseType: 'blob'
      });
  }
}
