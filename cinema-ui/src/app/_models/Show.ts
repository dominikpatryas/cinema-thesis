import {Hall} from './Hall';
import {SeatReserved} from './SeatReserved';

export interface Show {
  id: number;
  datePlayed: any;
  movieId: number;
  hallId: number;
  hall: Hall;
  seatsReserved: SeatReserved[];
}
