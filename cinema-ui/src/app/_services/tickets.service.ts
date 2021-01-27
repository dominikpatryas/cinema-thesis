import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Movie} from '../_models/Movie';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TicketsService {
  apiUrl = environment.apiUrl + 'tickets/';

  constructor(private http: HttpClient) { }

  getTicket(id: string) {
    return this.http.get(this.apiUrl + id, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }
}
