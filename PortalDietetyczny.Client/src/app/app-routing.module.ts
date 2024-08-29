import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './components/pages/start/start.component';
import { RoutesEnum } from './enums/RoutesEnum';
import { AboutUsComponent } from './components/pages/about-us/about-us.component';
import { CalculatorsComponent } from './components/pages/calculators/calculators.component';
import { RecipesComponent } from './components/pages/recipes/recipes.component';
import { AdminPanelComponent } from './components/pages/admin-panel/admin-panel.component';

const routes: Routes =
  [
    {path: RoutesEnum.start, component: StartComponent},
    {path: RoutesEnum.about, component: AboutUsComponent},
    {path: RoutesEnum.calculators, component: CalculatorsComponent},
    {path: RoutesEnum.recipes, component: RecipesComponent},
    {path: RoutesEnum.adminPanel, component: AdminPanelComponent},
    {path: "**", component: StartComponent}
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
