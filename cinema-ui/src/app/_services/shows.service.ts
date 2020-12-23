import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShowsService {
  apiUrl = environment.apiUrl + 'shows/';

  constructor(private http: HttpClient) { }

  getShow(id: string) {
    return this.http.get(this.apiUrl + id);
  }

  getShowsForMovie(id: string): Observable<any> {
    return this.http.get<any[]>(this.apiUrl + id + "/all");
  }

  getShows(): Observable<any> {
    return this.http.get<any[]>(this.apiUrl);
  }

  addShow(show: any) {
    return this.http.post(this.apiUrl, show, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  deleteShow(id: string) {
    return this.http.delete(this.apiUrl + id);
  }

  // updateMovie(movie: MovieForUpdate) {
  //   return this.http.patch(this.apiUrl + movie.id, movie);
  // }
}
