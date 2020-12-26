import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Movie} from '../_models/Movie';
import {Observable} from 'rxjs';

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

  addMovie(movie: Movie) {
    return this.http.post(this.apiUrl, movie, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  deleteMovie(id: string) {
    return this.http.delete(this.apiUrl + id, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  updateMovie(movie: Movie) {
    return this.http.patch(this.apiUrl + movie.id, movie, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }
}
