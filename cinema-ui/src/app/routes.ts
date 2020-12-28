import { Routes } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {RegisterComponent} from './register/register.component';
import {AboutUsComponent} from './about-us/about-us.component';
import {PriceListComponent} from './price-list/price-list.component';
import {ContactComponent} from './contact/contact.component';
import {MovieComponent} from './movie/movie.component';
import {AddShowComponent} from './adding-panel/show/add-show.component';
import {AddHallComponent} from './adding-panel/hall/add-hall.component';
import {AuthGuard} from './_guards/auth.guard';
import {AddMovieComponent} from './adding-panel/add-movie/add-movie.component';
import {ReservationsComponent} from './reservations/reservations.component';
import {MyProfileComponent} from './my-profile/my-profile.component';

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
      { path: 'add/show', component: AddShowComponent, canActivate: [AuthGuard]},
      { path: 'add/hall', component: AddHallComponent, canActivate: [AuthGuard]},
      { path: 'add/movie', component: AddMovieComponent, canActivate: [AuthGuard]},
      { path: 'reservations', component: ReservationsComponent, canActivate: [AuthGuard]},
      { path: 'profile', component: MyProfileComponent, canActivate: [AuthGuard]}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'},

];
