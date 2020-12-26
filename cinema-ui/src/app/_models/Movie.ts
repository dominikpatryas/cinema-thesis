import {Photo} from './Photo';
import {Cast} from './Cast';

export interface Movie {
  id: number;
  title: string;
  description: string;
  trailerUrl: string;
  year: number;
  types: [{type: string}];
  duration: number;
  photos: Photo[];
  casts: Cast[];
}
