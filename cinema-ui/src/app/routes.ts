import { Routes } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {RegisterComponent} from './register/register.component';
import {AboutUsComponent} from './about-us/about-us.component';
import {PriceListComponent} from './price-list/price-list.component';
import {ContactComponent} from './contact/contact.component';
import {MovieComponent} from './movie/movie.component';
import {AddShowComponent} from './adding-panel/show/add-show.component';
import {AddHallComponent} from './adding-panel/hall/add-hall.component';
import {AdminEmployeeGuard} from './_guards/admin-employee-guard.service';
import {AddMovieComponent} from './adding-panel/add-movie/add-movie.component';
import {ReservationsComponent} from './reservations/reservations.component';
import {MyProfileComponent} from './my-profile/my-profile.component';
import {LoggingGuard} from './_guards/LoggingGuard';
import {TicketsComponent} from './tickets/tickets.component';

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
      { path: 'add/show', component: AddShowComponent, canActivate: [AdminEmployeeGuard]},
      { path: 'add/hall', component: AddHallComponent, canActivate: [AdminEmployeeGuard]},
      { path: 'add/movie', component: AddMovieComponent, canActivate: [AdminEmployeeGuard]},
      { path: 'reservations', component: ReservationsComponent, canActivate: [AdminEmployeeGuard]},
      { path: 'profile', component: MyProfileComponent, canActivate: [LoggingGuard]},
      { path: 'tickets/:id', component: TicketsComponent, canActivate: [LoggingGuard]}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'},

];
