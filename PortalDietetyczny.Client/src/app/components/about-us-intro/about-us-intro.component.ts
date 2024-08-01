import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about-us-intro',
  templateUrl: './about-us-intro.component.html',
  styleUrl: './about-us-intro.component.css'
})
export class AboutUsIntroComponent implements OnInit
{
  public isBigScreen: boolean = false;

  ngOnInit(): void
  {
    window.addEventListener('resize', () => {
      console.log(window.innerWidth);

      this.isBigScreen = window.innerWidth <= 431;
    });
  }

}
