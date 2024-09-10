import { Component } from '@angular/core';
import { BlogService } from '../../../services/blog.service';
import { BlogPostDetailsDto } from '../../../DTOs/BlogPostDetailsDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent
{
  postDetails: BlogPostDetailsDto;
  uid: number

  constructor(private blogService: BlogService, private router: Router)
  {
    let link = this.router.url.split("-")
    this.uid = parseInt(link[link.length - 1], 16)
  }

  ngOnInit(): void
  {
    this.blogService.getBlogPostDetails(this.uid).subscribe(
      (response) =>
      {
        this.postDetails = response
        console.log(response)
      },
      (error) => console.log(error.error)
    );
    console.log("test: " + this.postDetails)
  }

  previousPage()
  {
    this.router.navigate(['/blog']);
  }
}
