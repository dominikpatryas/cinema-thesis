import {Photo} from './Photo';

export interface Movie {
  id: number;
  title: string;
  description: string;
  trailerUrl: string;
  photos: Photo[];
}
