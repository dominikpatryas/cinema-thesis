import { Routes } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {RegisterComponent} from './register/register.component';
import {AboutUsComponent} from './about-us/about-us.component';
import {PriceListComponent} from './price-list/price-list.component';
import {ContactComponent} from './contact/contact.component';
import {MovieComponent} from './movie/movie.component';
import {ShowComponent} from './adding-panel/show/show.component';
import {HallComponent} from './adding-panel/hall/hall.component';


export const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [
      { path: 'register', component: RegisterComponent},
      { path: 'about-us', component: AboutUsComponent},
      { path: 'price-list', component: PriceListComponent},
      { path: 'contact', component: ContactComponent},
      { path: 'movies/:id', component: MovieComponent},
      { path: 'add/show', component: ShowComponent},
      { path: 'add/hall', component: HallComponent},
      // { path: 'add/show', component: ShowComponent}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'},

];
