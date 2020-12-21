import {Photo} from './Photo';
import {Cast} from './Cast';

export interface Movie {
  id: number;
  title: string;
  description: string;
  trailerUrl: string;
  photos: Photo[];
  casts: Cast[];
}
