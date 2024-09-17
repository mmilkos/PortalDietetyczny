import { Component, OnInit } from '@angular/core';
import { FilesService } from '../../../services/files.service';
import { IdAndName } from '../../../DTOs/IdAndName';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-downloads-grid',
  templateUrl: './downloads-grid.component.html',
  styleUrl: './downloads-grid.component.css'
})
export class DownloadsGridComponent implements OnInit
{
  files: IdAndName[];
  constructor(private filesService: FilesService) {
  }

  ngOnInit(): void
  {
    this.filesService.getAllFiles().subscribe(
          (respnse)=> this.files = respnse.names,
          (error) => console.log(error.error))
  }

  getDownloadableFile(id: number)
  {
    this.filesService.getDownloadableFile(id).subscribe(
      (response: HttpResponse<Blob>) => {
        if (response.body) {
          let blob = new Blob([response.body], { type: 'application/pdf' });
          let url = window.URL.createObjectURL(blob);
          let contentDisposition = response.headers.get('Content-Disposition') || '';
          let fileName = contentDisposition.split(';')[1].split("=")[1];
          fileName = fileName.replace(/\./g, "-")
          let link = document.createElement('a');
          link.href = url;
          link.download = fileName;
          link.click();
        } else console.log("No response body");
      },
      (error) => console.log(error.error)
    );
  }

}
