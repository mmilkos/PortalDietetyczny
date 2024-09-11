import { Component } from '@angular/core';

@Component({
  selector: 'app-consultation-grid',
  templateUrl: './consultation-grid.component.html',
  styleUrl: './consultation-grid.component.css'
})
export class ConsultationGridComponent
{
  agaImg: string = "assets/agaTlo.JPG"
  anaImg: string = "assets/aniaTlo.JPG"

  agaName: string = "Agnieszka Miłkowska"
  aniaName: string = "Anna Juranek"

}
