import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddBlogPostDto } from '../DTOs/AddBlogPostDto';
import { PagedResult } from '../models/PagedResult';
import { BlogPostPreviewDto } from '../DTOs/BlogPostPreviewDto';
import { BlogPostsPreviewPageRequest } from '../DTOs/BlogPostsPreviewPageRequest';
import { BlogPostDetailsDto } from '../DTOs/BlogPostDetailsDto';
import { NamesListDto } from '../DTOs/IdAndName';
import { config } from '../config';

@Injectable({
  providedIn: 'root'
})
export class BlogService
{
  apiUrl: string = config.API_URL + "blog"
  constructor(private http: HttpClient) { }

  addBlogPost(post: AddBlogPostDto): Observable<any>
  {
    return this.http.post<any>(this.apiUrl, post, {withCredentials: true});
  }

  getBlogPostsPaged(params: BlogPostsPreviewPageRequest ) : Observable<PagedResult<BlogPostPreviewDto>>
  {
    return this.http.post<PagedResult<BlogPostPreviewDto>>(this.apiUrl + "/paged", params)
  }

  getBlogPostDetails(uid: string): Observable<BlogPostDetailsDto>
  {
    return this.http.get<BlogPostDetailsDto>(this.apiUrl + `/${uid}`)
  }

  getPosts(): Observable<NamesListDto>
  {
    return this.http.get<NamesListDto>(this.apiUrl)
  }

  deleteBlogPost(id: number): Observable<any>
  {
    return this.http.delete(this.apiUrl + `/${id}`)
  }


}
