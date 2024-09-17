import { Component, OnInit } from '@angular/core';
import { IdAndName } from '../../../DTOs/IdAndName';
import { FilesService } from '../../../services/files.service';
import { AddFileDto } from '../../../DTOs/AddFileDto';

@Component({
  selector: 'app-add-file-form',
  templateUrl: './add-file-form.component.html',
  styleUrl: './add-file-form.component.css'
})
export class AddFileFormComponent implements
  OnInit
{
  postContent: any;
  editorConfig: any
  postName: string = ""

  fileRealName: string = "";
  fileName: string = "";

  file: string;

  filesNames: IdAndName[] = [];
  selectedFileToDelete: number;

  constructor(private fileService: FilesService) {
  }

  ngOnInit(): void
  {
    this.fileService.getAllFiles().subscribe(
      (response)=> this.filesNames = response.names,
      (error)=> { console.log(error.error)}
    )
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
      this.file = btoa(binary);
    }

    reader.readAsArrayBuffer(file);
    this.fileName = file.name
  }

  Submit()
  {
    const dto: AddFileDto =
      {
       name: this.fileName,
        fileName: this.fileRealName,
        fileBytes: this.file
      }

    this.fileService.addFile(dto).subscribe(
      ()=> {},
      (error)=> { console.log(error.error)}
    )
  }

  deleteFile()
  {
    this.fileService.deleteFile(this.selectedFileToDelete).subscribe(
      ()=> {},
    (error)=> console.log(error.error)
    )
  }
}
