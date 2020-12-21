import {Component, Input, OnInit} from '@angular/core';
import {Movie} from '../_models/Movie';
import {MoviesService} from '../_services/movies.service';
import {ActivatedRoute} from '@angular/router';
import {AlertifyService} from '../_services/alertify.service';
import {ShowsService} from '../_services/shows.service';
import {DateService} from '../_services/date.service';
import {ReservationsService} from "../_services/reservations.service";
import {AuthService} from "../_services/auth.service";

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {
  movie: Movie;
  shows: any[];
  show: any;
  seatsToBeReserved = [];
  seatsReserved = [];
  isReservationVisible = false;
  displayedColumns: string[] = ['Datetime of show', 'Available seats', 'Reservation'];

  constructor(private moviesService: MoviesService,
              private route: ActivatedRoute,
              private alertify: AlertifyService,
              private showsService: ShowsService,
              private authService: AuthService,
              private reservationsService: ReservationsService,
              public dateService: DateService) { }

  ngOnInit(): void {
    this.loadMovie();
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
      console.log(this.shows);
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
        console.log('seatsAlreadyReserved', this.seatsReserved);
        console.log(show);
      }, error => {
        this.alertify.error('Error in loading show');
      });
    }
  }

  chooseSeat(seatNumber) {
    if (this.seatsToBeReserved.includes(seatNumber)) {
      this.removeSeat(seatNumber);
    } else {
      this.seatsToBeReserved.push(seatNumber);
    }

    console.log(this.seatsToBeReserved);
  }

  removeSeat(seatNumber) {
    const indexOfSeat = this.seatsToBeReserved.indexOf(seatNumber);

    if (indexOfSeat > -1){
      this.seatsToBeReserved.splice(indexOfSeat, 1);
    }
  }

  createReservation() {
    if (this.seatsToBeReserved.length) {
      this.seatsToBeReserved.forEach((seat, i) => {
        this.seatsToBeReserved[i] = { seatNumber: seat };
      });

      this.reservationsService.addReservation(Number(this.authService.dekodedToken.nameid), this.show.id, this.seatsToBeReserved).subscribe(res => {
        this.seatsToBeReserved.forEach(seatToBeReserved => {
          this.seatsReserved.push(seatToBeReserved.seatNumber);
        });
        this.seatsToBeReserved = [];

        this.alertify.success(`Succesfully reservation for ${this.movie.title}`);
      }, (error) => {
        console.log(error);
        this.alertify.error(`Failed reservation for ${this.movie.title}`);
      });
    }
  }
}
