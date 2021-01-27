import {Component, Input, OnInit} from '@angular/core';
import {Movie} from '../_models/Movie';
import {MoviesService} from '../_services/movies.service';
import {ActivatedRoute} from '@angular/router';
import {AlertifyService} from '../_services/alertify.service';
import {ShowsService} from '../_services/shows.service';
import {DateService} from '../_services/date.service';
import {ReservationsService} from '../_services/reservations.service';
import {AuthService} from '../_services/auth.service';
import {faCalendarAlt} from '@fortawesome/free-solid-svg-icons';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import * as uuid from 'uuid';
import {Show} from '../_models/Show';
@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {
  // icons
  faCalendarAlt = faCalendarAlt;

  movie: Movie;
  shows: Show[];
  show: Show;
  seatsToBeReserved = [];
  seatsReserved = [];
  isReservationVisible = false;
  displayedColumns: string[] = ['Datetime of show', 'Available seats', 'Reservation'];
  reservationForm: FormGroup;
  reducedTicketsCount = 0;

  constructor(private moviesService: MoviesService,
              private route: ActivatedRoute,
              private alertify: AlertifyService,
              private showsService: ShowsService,
              private authService: AuthService,
              private reservationsService: ReservationsService,
              public dateService: DateService,
              private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.loadMovie();
    this.createReservationForm();
  }

  isUserLoggedIn() {
    return this.authService.isLoggedIn();
  }

  loadMovie() {
    this.moviesService.getMovie(this.route.snapshot.params.id).subscribe((movie: Movie) => {
      this.movie = movie;

      this.loadShowsForMovie();
    }, error => {
      this.alertify.error('Error in loading movie');
    });
  }

  loadShowsForMovie() {
    this.showsService.getShowsForMovie(this.route.snapshot.params.id).subscribe(shows => {
      this.shows = shows;
    }, error => {
      this.alertify.error('Error in loading shows for movie');
    });
  }

  setVisibilityOfReservation(visibility: boolean) {
    this.isReservationVisible = visibility;
  }

  getShow(id: number) {
    if (this.show?.id !== id) {
      this.seatsReserved = [];
      this.seatsToBeReserved = [];

      this.showsService.getShow(id.toString()).subscribe(show => {
        this.show = show;

        this.show.seatsReserved.forEach((seat) => {
          this.seatsReserved.push(seat.seatNumber);
        });
      }, error => {
        this.alertify.error('Error in loading show');
      });
    }
  }

  chooseSeat(seatNumber) {
    if (this.seatsReserved.includes(seatNumber)) {
      return;
    }

    this.seatsToBeReserved.includes(seatNumber) ? this.removeSeat(seatNumber) : this.seatsToBeReserved.push(seatNumber);
  }

  removeSeat(seatNumber) {
    const indexOfSeat = this.seatsToBeReserved.indexOf(seatNumber);

    if (indexOfSeat > -1) {
      this.seatsToBeReserved.splice(indexOfSeat, 1);
    }
  }

  createReservation(userId?) {
    if (this.seatsToBeReserved.length) {
      this.seatsToBeReserved.forEach((seat, i) => {
        this.seatsToBeReserved[i] = {seatNumber: seat};
      });

      if (!userId) {
        userId = Number(this.authService.getDecodedToken()?.nameid);
      }

      this.reservationsService.addReservation(userId, this.show.id, this.seatsToBeReserved, this.reducedTicketsCount, this.seatsToBeReserved.length - this.reducedTicketsCount).subscribe(res => {
        this.seatsToBeReserved.forEach(seatToBeReserved => {
          this.seatsReserved.push(seatToBeReserved.seatNumber);
        });
        this.seatsToBeReserved = [];
        this.reducedTicketsCount = 0;

        this.alertify.success(`Succesfully reservation for ${this.movie.title}`);
      }, (error) => {
        console.log(error);
        this.alertify.error(`Failed reservation for ${this.movie.title}`);
      });
    }
  }

  createReservationForm() {
    this.reservationForm = this.formBuilder.group({
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
    });
  }

  createReservationWithoutAccount() {
    if (this.reservationForm.valid) {
      const reservationUser = Object.assign({}, this.reservationForm.value);

      reservationUser.password = uuid.v4();
      reservationUser.temporaryReservation = true;
      this.authService.register(reservationUser).subscribe((createdReservationUserId: any) => {

        this.createReservation(createdReservationUserId);
      }, error => {
        this.alertify.error('Error while adding reservation');
      });
    }
  }
}
