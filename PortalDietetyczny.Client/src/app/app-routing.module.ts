import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './components/pages/start/start.component';
import { RoutesEnum } from './enums/RoutesEnum';
import { AboutUsComponent } from './components/pages/about-us/about-us.component';
import { CalculatorsComponent } from './components/pages/calculators/calculators.component';
import { RecipesComponent } from './components/pages/recipes/recipes.component';
import { AdminPanelComponent } from './components/pages/admin-panel/admin-panel.component';
import { RecipeComponent } from './components/pages/recipe/recipe.component';
import { BlogsComponent } from './components/pages/blogs/blogs.component';
import { BlogComponent } from './components/pages/blog/blog.component';
import { ConsultationsComponent } from './components/pages/consultations/consultations.component';
import { DownloadsComponent } from './components/pages/downloads/downloads.component';
import { LoginComponent } from './components/pages/login/login.component';
import { DietsComponent } from './components/pages/diets/diets.component';
import { CartComponent } from './components/pages/cart/cart.component';
import { TermsOfServiceComponent } from './components/pages/terms-of-service/terms-of-service.component';



const routes: Routes =
  [
    {path: RoutesEnum.start, component: StartComponent},
    {path: RoutesEnum.about, component: AboutUsComponent},
    {path: RoutesEnum.recipes, component: RecipesComponent},
    {path: RoutesEnum.recipes + "/:recipeName", component: RecipeComponent},
    {path: RoutesEnum.diets, component: DietsComponent},
    {path: RoutesEnum.calculators, component: CalculatorsComponent},
    {path: RoutesEnum.blog, component: BlogsComponent},
    {path: RoutesEnum.blog + "/:postName", component: BlogComponent},
    {path: RoutesEnum.consultations, component: ConsultationsComponent},
    {path: RoutesEnum.adminPanel, component: AdminPanelComponent},
    {path: RoutesEnum.downloads, component: DownloadsComponent},
    {path: RoutesEnum.login, component: LoginComponent},
    {path: RoutesEnum.shopping_cart, component: CartComponent},
    {path: RoutesEnum.terms_of_service, component: TermsOfServiceComponent},
    {path: "**", component: StartComponent}
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
