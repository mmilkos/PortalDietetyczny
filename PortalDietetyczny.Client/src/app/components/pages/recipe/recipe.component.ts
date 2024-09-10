import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipesService } from '../../../services/recipes.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.css'
})
export class RecipeComponent implements OnInit
{
  uid: number;

  constructor(private router: Router) {
  }

  ngOnInit(): void
  {
        let link = this.router.url.split("-")
        this.uid = parseInt(link[link.length - 1], 16)
    }

}
