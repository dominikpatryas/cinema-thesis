import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {SeatReserved} from '../_models/SeatReserved';
import {Observable} from 'rxjs';
import {Reservation} from '../_models/Reservation';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {
  apiUrl = environment.apiUrl + 'reservations/';

  constructor(private http: HttpClient) { }

  addReservation(userId: number, showId: number, seatsReserved: SeatReserved[], reducedTickets: number, normalTickets: number) {
    return this.http.post(this.apiUrl, {userId, showId, seatsReserved, reducedTickets, normalTickets});
  }

  getManagementReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl + 'management', {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  confirmReservation(id: number) {
    return this.http.patch(this.apiUrl + id,{ }, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  declineReservation(id: number) {
    return this.http.delete(this.apiUrl + id, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }
}
