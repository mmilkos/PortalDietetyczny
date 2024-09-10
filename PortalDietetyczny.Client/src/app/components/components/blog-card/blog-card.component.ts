import { Component, Input } from '@angular/core';
import { BlogPostPreviewDto } from '../../../DTOs/BlogPostPreviewDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blog-card',
  templateUrl: './blog-card.component.html',
  styleUrl: './blog-card.component.css'
})
export class BlogCardComponent
{
  @Input() info: BlogPostPreviewDto;

  constructor(private router: Router) {
  }

  navigateToPost()
  {
    let link : string = this.router.url + "/" + this.info.url;
    this.router.navigateByUrl(link)
  }
}
