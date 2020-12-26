import {Component, OnInit} from '@angular/core';
import {ReservationsService} from '../_services/reservations.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {
  reservations: any[];
  displayedColumns: string[] = ['User id', 'Show id', 'Seats reserved', 'Confirm'];

  constructor(private reservationsService: ReservationsService) {
  }

  ngOnInit(): void {
    this.getReservations();
  }

  getReservations() {
    this.reservationsService.getReservations().subscribe(reservations => {
      this.reservations = reservations.filter( reservation => !reservation.isConfirmed);
    });
  }

  confirmReservation(id: number) {
    this.reservationsService.confirmReservation(id).subscribe(response => {
    });
  }

  declineReservation(id: number) {
    this.reservationsService.confirmReservation(id).subscribe(response => {
    });
  }

}
