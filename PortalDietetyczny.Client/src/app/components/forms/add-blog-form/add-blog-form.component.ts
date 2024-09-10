import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { BlogService } from '../../../services/blog.service';
import { AddBlogPostDto } from '../../../DTOs/AddBlogPostDto';

@Component({
  selector: 'app-add-blog-form',
  templateUrl: './add-blog-form.component.html',
  styleUrls: ['./add-blog-form.component.css']
})
export class AddBlogFormComponent implements
  OnInit, AfterViewInit {
  @ViewChild('editor')
  editorElement!: ElementRef;
  postContent: any;
  editorConfig: any
  postName: string = ""

  fileName: string = "";

  photo: string;


  constructor(private blogService: BlogService) {}

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files[0];

    const reader = new FileReader();

    reader.onload = (e: any) =>
    {
      const arrayBuffer = e.target.result;
      const bytes = new Uint8Array(arrayBuffer);
      let binary = '';
      bytes.forEach((byte) => binary += String.fromCharCode(byte));
      this.photo = btoa(binary);
    }

    reader.readAsArrayBuffer(file);
    this.fileName = file.name
  }

  Submit()
  {
    const dto: AddBlogPostDto =
      {
        title: this.postName,
        content: this.postContent,
        fileBytes: this.photo,
        fileName: this.fileName,
      }

      this.blogService.addBlogPost(dto).subscribe(
        ()=> {},
        (error)=> { console.log(error.error)}
      )
  }
}
