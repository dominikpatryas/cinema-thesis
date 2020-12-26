import {CanActivate, Router} from '@angular/router';
import {AuthService} from '../_services/auth.service';
import {AlertifyService} from '../_services/alertify.service';
import {Injectable} from '@angular/core';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(): boolean {
    const decodedToken = this.authService.getDecodedToken();

    if (decodedToken.Admin || decodedToken.Employee ) {
      return true;
    }

    this.router.navigate(['/']);
    return false;
  }
}
