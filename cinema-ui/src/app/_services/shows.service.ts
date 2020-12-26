import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Show} from '../_models/Show';

@Injectable({
  providedIn: 'root'
})
export class ShowsService {
  apiUrl = environment.apiUrl + 'shows/';

  constructor(private http: HttpClient) { }

  getShow(id: string): Observable<Show> {
    return this.http.get<Show>(this.apiUrl + id);
  }

  getShowsForMovie(id: string): Observable<Show[]> {
    return this.http.get<Show[]>(this.apiUrl + id + '/all');
  }

  getShows(): Observable<Show[]> {
    return this.http.get<Show[]>(this.apiUrl);
  }

  addShow(show: Show) {
    return this.http.post(this.apiUrl, show, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  deleteShow(id: string) {
    return this.http.delete(this.apiUrl + id);
  }
}
