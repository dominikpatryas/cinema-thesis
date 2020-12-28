import { Injectable } from '@angular/core';
import {User} from '../_models/User';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
import {UserForLogin} from '../_models/UserForLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  base = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  currentUser: User;
  constructor(private http: HttpClient) { }

  logIn(model: UserForLogin | User) {
    return this.http.post(this.base + 'login', model)
      .pipe(map((response: any) =>  {
          if (response) {
            localStorage.setItem('token', response.token);
            localStorage.setItem('userId', JSON.stringify(response.userId));
          }
        }
      ));
  }
  register(user: User) {
    return this.http.post(this.base + 'register', user);
  }

  isLoggedIn() {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }

  getUserReservations() {
    return this.http.get(this.base + 'reservations', {headers: new HttpHeaders('Authorization: Bearer ' + localStorage.getItem('token'))} );
  }

  getDecodedToken() {
    const token = localStorage.getItem('token');

    if (token) {
      return this.jwtHelper.decodeToken(localStorage.getItem('token'));
    }

    return false;
  }

  isAdmin() {
    return this.getDecodedToken().Admin;
  }

  isEmployee() {
    return this.getDecodedToken().Employee;
  }

  isAdminOrEmployee() {
    return this.isEmployee() || this.isAdmin();
  }
}
