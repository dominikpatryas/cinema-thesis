import {Photo} from './Photo';

export interface Movie {
  id: number;
  title: string;
  description: string;
  trailerUrl: string;
  datePlayed: Date;
  photos: Photo[];
  mainPhoto: Photo;
}
