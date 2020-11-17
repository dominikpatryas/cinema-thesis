import {Photo} from './Photo';

export interface MovieForAdd {
  title: string;
  description: string;
  trailerUrl: string;
  datePlayed: Date;
  photos?: Photo[];
}
