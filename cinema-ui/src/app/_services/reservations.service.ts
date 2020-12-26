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

  addReservation(userId: number, showId: number, seatsReserved: SeatReserved[]) {
    return this.http.post(this.apiUrl, {userId, showId, seatsReserved});
  }

  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl);
  }

  confirmReservation(id: number) {
    return this.http.patch(this.apiUrl, id);
  }

  declineReservation(id: number) {
    return this.http.post(this.apiUrl + 'remove', {id});
  }
}
