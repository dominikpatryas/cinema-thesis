import {Component, OnInit} from '@angular/core';
import {ReservationsService} from '../_services/reservations.service';
import {AlertifyService} from '../_services/alertify.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {
  confirmedReservations: any[];
  reservations: any[];
  displayedColumns: string[] = [
    'Email',
    'First name',
    'Last name',
    'Date played',
    'Seats reserved',
    'Confirm'
  ];

  constructor(private reservationsService: ReservationsService, private alertifyService: AlertifyService) {
  }

  ngOnInit(): void {
    this.getReservations();
  }

  getManagementReservations() {
    this.reservationsService.getManagementReservations().subscribe(reservations => {
      this.reservations = reservations;
    });
  }

  getReservations() {
    this.reservationsService.getReservations().subscribe(reservations => {
      this.reservations = reservations.filter(reservation => !reservation.isConfirmed);
      this.confirmedReservations = reservations.filter(reservation => reservation.isConfirmed);
    });
  }

  confirmReservation(id: number) {
    this.reservationsService.confirmReservation(id).subscribe(response => {
      this.getReservations();
      this.alertifyService.success('Successfully confirmed a reservation');
    });
  }

  declineReservation(id: number) {
    this.reservationsService.declineReservation(id).subscribe(response => {
      this.getReservations();
      this.alertifyService.success('Successfully deleted a reservation');
    });
  }

}
