import {Component, OnInit} from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {Reservation} from '../_models/Reservation';
import {User} from '../_models/User';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Show id', 'Seats reserved', 'Confirmed', 'Ticket'];
  user: User;

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.getUserReservations().subscribe((user: User) => {
      this.user = user;
    });
  }

  getTicket(id: number) {
    // TODO
  }
}
