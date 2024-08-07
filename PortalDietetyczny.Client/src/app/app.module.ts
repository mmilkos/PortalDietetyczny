import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { StartComponent } from './components/pages/start/start.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material/card';
import { AboutUsComponent } from './components/pages/about-us/about-us.component';
import { CalculatorsComponent } from './components/pages/calculators/calculators.component';
import { BmiCalculatorComponent } from './components/calculators/bmi-calculator/bmi-calculator.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PpmCalculatorComponent } from './components/calculators/ppm-calculator/ppm-calculator.component';
import { WhrCalculatorComponent } from './components/calculators/whr-calculator/whr-calculator.component';
import { HeaderComponent } from './components/components/header/header.component';
import { AboutCardComponent } from './components/components/about-card/about-card.component';
import { DetailsGridComponent } from './components/grids/details-grid/details-grid.component';
import { DetailsCardComponent } from './components/components/details-card/details-card.component';
import { FooterComponent } from './components/components/footer/footer.component';
import { AboutUsIntroComponent } from './components/components/about-us-intro/about-us-intro.component';
import { CalculatorsGridComponent } from './components/grids/calculators-grid/calculators-grid.component';
import { CalculatorCardComponent } from './components/components/calculator-card/calculator-card.component';
import { CpmCalculatorComponent } from './components/calculators/cpm-calculator/cpm-calculator.component';
import { NmcCalculatorComponent } from './components/calculators/nmc-calculator/nmc-calculator.component';
import { RecipesComponent } from './components/pages/recipes/recipes.component';
import { RecipesGridComponent } from './components/grids/recipes-grid/recipes-grid.component';
import { RecipeCardComponent } from './components/components/recipe-card/recipe-card.component';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    StartComponent,
    AboutCardComponent,
    DetailsGridComponent,
    DetailsCardComponent,
    FooterComponent,
    AboutUsComponent,
    AboutUsIntroComponent,
    CalculatorsComponent,
    CalculatorsGridComponent,
    BmiCalculatorComponent,
    PpmCalculatorComponent,
    WhrCalculatorComponent,
    CalculatorCardComponent,
    CpmCalculatorComponent,
    NmcCalculatorComponent,
    RecipesComponent,
    RecipesGridComponent,
    RecipeCardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BsDropdownModule.forRoot(),
    MatSlideToggleModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
