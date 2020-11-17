import { Component, OnInit } from '@angular/core';
import {environment} from '../../environments/environment';
import {Movie} from '../_models/Movie';
import {HttpClient} from '@angular/common/http';
import {MoviesService} from '../_services/movies.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  movies: Movie[];
  constructor(private http: HttpClient, private moviesService: MoviesService) { }

  ngOnInit(): void {
    this.moviesService.getMovies().subscribe(movies => {
      this.movies = movies;
    });
  }

}
