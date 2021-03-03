import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AlertifyService} from '../../_services/alertify.service';
import {MoviesService} from '../../_services/movies.service';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.scss']
})
export class AddMovieComponent implements OnInit {
  movieForm: FormGroup;

  constructor(private moviesService: MoviesService, private fb: FormBuilder, private alertifyService: AlertifyService) {
  }

  ngOnInit(): void {
    this.createMovieForm();
  }

  createMovieForm() {
    this.movieForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      trailerUrl: ['', Validators.required],
      year: [null, Validators.required],
      duration: [null, Validators.required],
      casts: ['', Validators.required],
      photos: ['', Validators.required],
      types: ['', Validators.required]
    });
  }

  addMovie() {
    if (this.movieForm.valid) {
      const movie = Object.assign({}, this.movieForm.value);

      movie.casts = this.convertCasts(movie.casts);
      movie.photos = this.convertPhotos(movie.photos);
      movie.types = this.convertTypes(movie.types);
      movie.trailerUrl = movie.trailerUrl.replace('watch?v=', 'embed/');

      this.moviesService.addMovie(movie).subscribe(() => {
        this.alertifyService.success('Added movie successfully.');
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }

  concatMovieValues(commaSeparatedString: string) {
    return commaSeparatedString.split(',');
  }

  convertCasts(casts: string) {
    return casts.split(',').map(cast => {
      const firstLastName = cast.split(' ');

      return {
        firstName: firstLastName[0],
        lastName: firstLastName
          .filter(x => x !== firstLastName[0])
          .join(' ')
      };
    });
  }

  convertPhotos(photos: string) {
    return photos.split(',').map( (photoUrl, index) => {
      return {
        url: photoUrl,
        isMain: index === 0
      };
    });
  }

  convertTypes(types: string) {
    return types.split(',').map((type) => {
      return {
        type
      };
    });
  }
}
