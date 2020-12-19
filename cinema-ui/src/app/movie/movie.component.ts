import {Component, Input, OnInit} from '@angular/core';
import {Movie} from '../_models/Movie';
import {MoviesService} from '../_services/movies.service';
import {ActivatedRoute} from '@angular/router';
import {AlertifyService} from '../_services/alertify.service';
import {ShowsService} from '../_services/shows.service';
import {DateService} from '../_services/date.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  movie: Movie;
  shows: any[];
  show: any;
  isReservationVisible = false;
  displayedColumns: string[] = ['Datetime of show', 'Available seats', 'Reservation'];
  constructor(private moviesService: MoviesService,
              private route: ActivatedRoute,
              private alertify: AlertifyService,
              private showsService: ShowsService,
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
    this.showsService.getShow(id.toString()).subscribe(show => {
      this.show = show;
      console.log(show);
    }, error => {
      this.alertify.error('Error in loading show');
    });
  }
}
