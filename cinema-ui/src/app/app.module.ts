import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {HomeComponent} from './home/home.component';
import {FooterComponent} from './footer/footer.component';
import {AboutUsComponent} from './about-us/about-us.component';
import {RegisterComponent} from './register/register.component';
import {NavigationComponent} from './navigation/navigation.component';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {appRoutes} from './routes';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { PriceListComponent } from './price-list/price-list.component';
import { ContactComponent } from './contact/contact.component';
import { MovieComponent } from './movie/movie.component';
import {MatTableModule} from '@angular/material/table';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {AddShowComponent} from './adding-panel/show/add-show.component';
import { AddHallComponent } from './adding-panel/hall/add-hall.component';
import {AdminEmployeeGuard} from './_guards/admin-employee-guard.service';
import { AddMovieComponent } from './adding-panel/add-movie/add-movie.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { VideoComponent } from './video/video.component';
import {SafeVideoPipe} from './_pipes/SafeVideoPipe';
import { MyProfileComponent } from './my-profile/my-profile.component';
import {LoggingGuard} from './_guards/LoggingGuard';
import { TicketsComponent } from './tickets/tickets.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FooterComponent,
    AboutUsComponent,
    RegisterComponent,
    NavigationComponent,
    PriceListComponent,
    ContactComponent,
    MovieComponent,
    AddShowComponent,
    AddHallComponent,
    AddMovieComponent,
    ReservationsComponent,
    VideoComponent,
    SafeVideoPipe,
    MyProfileComponent,
    TicketsComponent
  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        RouterModule.forRoot(appRoutes),
        MatTableModule,
        FontAwesomeModule,
    ],
  providers: [
    AdminEmployeeGuard,
    LoggingGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
