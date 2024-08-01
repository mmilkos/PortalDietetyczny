import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './components/pages/start/start.component';
import { RoutesEnum } from './enums/RoutesEnum';
import { AboutUsComponent } from './components/pages/about-us/about-us.component';

const routes: Routes =
  [
    {path: RoutesEnum.start, component: StartComponent},
    {path: RoutesEnum.about, component: AboutUsComponent},
    {path: "**", component: StartComponent}
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
