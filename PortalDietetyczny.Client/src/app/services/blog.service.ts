import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddBlogPostDto } from '../DTOs/AddBlogPostDto';
import { PagedResult } from '../models/PagedResult';
import { BlogPostPreviewDto } from '../DTOs/BlogPostPreviewDto';
import { BlogPostsPreviewPageRequest } from '../DTOs/BlogPostsPreviewPageRequest';
import { BlogPostDetailsDto } from '../DTOs/BlogPostDetailsDto';

@Injectable({
  providedIn: 'root'
})
export class BlogService
{
  apiUrl: string = "https://localhost:44317/api/blog"
  constructor(private http: HttpClient) { }

  addBlogPost(post: AddBlogPostDto): Observable<any>
  {
    console.log(post)
    return this.http.post<any>(this.apiUrl, post);
  }

  getBlogPostsPaged(params: BlogPostsPreviewPageRequest ) : Observable<PagedResult<BlogPostPreviewDto>>
  {
    return this.http.post<PagedResult<BlogPostPreviewDto>>(this.apiUrl + "/paged", params)
  }

  getBlogPostDetails(uid: number): Observable<BlogPostDetailsDto>
  {
    return this.http.get<BlogPostDetailsDto>(this.apiUrl + `/${uid}`)
  }


}
