import { Component, OnInit } from '@angular/core';
import { BlogPostPreviewDto } from '../../../DTOs/BlogPostPreviewDto';
import { BlogService } from '../../../services/blog.service';
import { BlogPostsPreviewPageRequest } from '../../../DTOs/BlogPostsPreviewPageRequest';

@Component({
  selector: 'app-blog-grid',
  templateUrl: './blog-grid.component.html',
  styleUrl: './blog-grid.component.css'
})
export class BlogGridComponent implements OnInit
{
  dummyPost: BlogPostPreviewDto =
    {
      id: null,
      url: "",
      photoUrl: "https://res.cloudinary.com/dzohpx1mq/image/upload/v1726062320/Stock/hyui3cahzqmujvt1jlx5.gif",
      title: null
    }

    dummyPosts: BlogPostPreviewDto[] =
      [
        this.dummyPost,
        this.dummyPost,
        this.dummyPost,
        this.dummyPost,
        this.dummyPost,
        this.dummyPost,
      ]

  posts: BlogPostPreviewDto[] = this.dummyPosts;

  currentPage: number = 1;
  totalPages: number

  constructor(private recipesService: BlogService) {}

  ngOnInit(): void
  {
    var initDto : BlogPostsPreviewPageRequest =
      {
        PageNumber: this.currentPage,
        PageSize: 6
      }

      this.getPosts(initDto)
  }

  getPosts(dto: BlogPostsPreviewPageRequest)
  {
    this.recipesService.getBlogPostsPaged(dto).subscribe(
      (response) =>
      {
        this.posts = response.data;
        this.totalPages = Math.ceil(response.totalCount / 6)
      },
      (error) => console.log(error))
  }

  nextPage()
  {
    if (this.currentPage + 1 <= this.totalPages) {
      this.currentPage++;
    }

    this.posts = this.dummyPosts

    this.getPosts({
      PageNumber: this.currentPage,
      PageSize: 6,
    });

    window.scrollTo(0, 0);
  }

  previousPage()
  {
    this.currentPage--;
    if (this.currentPage < 1) this.currentPage = 1

    this.posts = this.dummyPosts

    this.getPosts({
      PageNumber: this.currentPage,
      PageSize: 6,
    });

    window.scrollTo(0, 0);
  }
}
