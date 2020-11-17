import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {MovieForAdd} from '../_models/MovieForAdd';
import {MovieForUpdate} from '../_models/MovieForUpdate';
import {Movie} from "../_models/Movie";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MoviesService {
  apiUrl = environment.apiUrl + 'movies/';

  constructor(private http: HttpClient) { }

  getMovie(id: string) {
    return this.http.get(this.apiUrl + id);
  }

  getMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this.apiUrl);
  }

  addMovie(movie: MovieForAdd) {
    return this.http.post(this.apiUrl, movie);
  }

  deleteMovie(id: string) {
    return this.http.delete(this.apiUrl + id);
  }

  updateMovie(movie: MovieForUpdate) {
    return this.http.patch(this.apiUrl + movie.id, movie);
  }
}
