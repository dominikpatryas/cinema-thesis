import {SeatReserved} from './SeatReserved';

export interface Reservation {
  id: number;
  userId: number;
  showId: number;
  seatsReserved: SeatReserved[];
  isConfirmed: boolean;
}
