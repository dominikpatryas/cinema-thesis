import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {
  apiUrl = environment.apiUrl + 'reservations/';

  constructor(private http: HttpClient) { }

  addReservation(userId: number, showId: number, seatsReserved: any[]) {
    return this.http.post(this.apiUrl, {userId, showId, seatsReserved},
      {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }
}
