import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent
{

  openLinkedin() {
    window.open('https://www.linkedin.com/in/mateusz-milkowski2002/', '_blank');
  }
}
