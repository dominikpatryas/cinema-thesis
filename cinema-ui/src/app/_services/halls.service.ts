import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Hall} from '../_models/Hall';
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class HallsService {
  apiUrl = environment.apiUrl + 'halls/';

  constructor(private http: HttpClient) { }

  getHall(id: string) {
    return this.http.get(this.apiUrl + id);
  }

  getHalls(): Observable<Hall[]> {
    return this.http.get<Hall[]>(this.apiUrl);
  }

  addHall(hall: Hall) {
    return this.http.post(this.apiUrl, hall, {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))});
  }

  deleteHall(id: string) {
    return this.http.delete(this.apiUrl + id);
  }
}
