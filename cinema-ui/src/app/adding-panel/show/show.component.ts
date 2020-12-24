import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../_services/auth.service';
import {AlertifyService} from '../../_services/alertify.service';
import {Router} from '@angular/router';
import {ShowsService} from '../../_services/shows.service';
import {MoviesService} from '../../_services/movies.service';
import {Movie} from '../../_models/Movie';
import {Hall} from '../../_models/Hall';
import {HallsService} from '../../_services/halls.service';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.scss']
})
export class ShowComponent implements OnInit {
  constructor(private authService: AuthService, private alertifyService: AlertifyService, private fb: FormBuilder,
              private route: Router,
              private showsService: ShowsService,
              private hallsService: HallsService,
              private moviesService: MoviesService) { }
  showForm: FormGroup;
  movies: Movie[];
  halls: Hall[];

  ngOnInit(): void {
    this.getMovies();
    this.getHalls();

    this.createShowForm();
  }

  createShowForm() {
    this.showForm = this.fb.group({
      movieId: [null, Validators.required],
      hallId: [null, Validators.required],
      datePlayed: [null, Validators.required],
    });
  }

  addShow() {
    if (this.showForm.valid) {
      const show = Object.assign({}, this.showForm.value);
      this.showsService.addShow(show).subscribe(() => {
        this.alertifyService.success('Added show successfully.');
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }

  getMovies() {
    this.moviesService.getMovies().subscribe(movies => { this.movies = movies; });
  }

  getHalls() {
    this.hallsService.getHalls().subscribe(halls => { this.halls = halls; console.log(halls) });
  }
}
