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
import { AdminPanelComponent } from './components/pages/admin-panel/admin-panel.component';
import { AddRecipeFormComponent } from './components/forms/add-recipe-form/add-recipe-form.component';
import {MatChipsModule} from '@angular/material/chips';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { AddTagFormComponent } from './components/forms/add-tag-form/add-tag-form.component';
import { IngredientsListComponent } from './components/forms/ingredients-list/ingredients-list.component';
import { FileUploadModule } from 'ng2-file-upload';
import { RecipeDetailsComponent } from './components/components/recipe-details/recipe-details.component';
import { RecipeComponent } from './components/pages/recipe/recipe.component';
import { AddBlogFormComponent } from './components/forms/add-blog-form/add-blog-form.component';
import { QuillModule } from 'ngx-quill';
import { BlogGridComponent } from './components/grids/blog-grid/blog-grid.component';
import { BlogCardComponent } from './components/components/blog-card/blog-card.component';
import { BlogDetailsComponent } from './components/components/blog-details/blog-details.component';
import { BlogsComponent } from './components/pages/blogs/blogs.component';
import { BlogComponent } from './components/pages/blog/blog.component';
import { ConsultationsComponent } from './components/pages/consultations/consultations.component';
import { NavGridComponent } from './components/grids/nav-grid/nav-grid.component';
import { ConsultationCardComponent } from './components/components/consultation-card/consultation-card.component';
import { ConsultationGridComponent } from './components/grids/consultation-grid/consultation-grid.component';
import { ConsultationIntroComponent } from './components/components/consultation-intro/consultation-intro.component';
import { AddFileFormComponent } from './components/forms/add-file-form/add-file-form.component';
import { DownloadsComponent } from './components/pages/downloads/downloads.component';
import { DownloadsGridComponent } from './components/grids/downloads-grid/downloads-grid.component';
import { LoginComponent } from './components/pages/login/login.component';
import { AddDietFormComponent } from './components/forms/add-diet-form/add-diet-form.component';
import { DietsComponent } from './components/pages/diets/diets.component';
import { DietsGridComponent } from './components/grids/diets-grid/diets-grid.component';
import { DietCardComponent } from './components/components/diet-card/diet-card.component';
import { CartComponent } from './components/pages/cart/cart.component';
import { ShopGridComponent } from './components/grids/shop-grid/shop-grid.component';
import { CartCardComponent } from './components/components/cart-card/cart-card.component';
import { TermsOfServiceComponent } from './components/pages/terms-of-service/terms-of-service.component';
import { TermsOfServiceIntroComponent } from './components/components/terms-of-service-intro/terms-of-service-intro.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    StartComponent,
    AboutCardComponent,
    NavGridComponent,
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
    AdminPanelComponent,
    AddRecipeFormComponent,
    IngredientsListComponent,
    AddTagFormComponent,
    RecipeDetailsComponent,
    RecipeComponent,
    AddBlogFormComponent,
    BlogGridComponent,
    BlogCardComponent,
    BlogDetailsComponent,
    BlogsComponent,
    BlogComponent,
    ConsultationsComponent,
    ConsultationCardComponent,
    ConsultationGridComponent,
    ConsultationIntroComponent,
    AddFileFormComponent,
    DownloadsComponent,
    DownloadsGridComponent,
    LoginComponent,
    AddDietFormComponent,
    DietsComponent,
    DietsGridComponent,
    DietCardComponent,
    CartComponent,
    ShopGridComponent,
    CartCardComponent,
    TermsOfServiceComponent,
    TermsOfServiceIntroComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BsDropdownModule.forRoot(),
    MatSlideToggleModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatChipsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    FileUploadModule,
    QuillModule.forRoot()
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
