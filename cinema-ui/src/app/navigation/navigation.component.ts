import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../_services/auth.service';
import {AlertifyService} from '../_services/alertify.service';
import {Router} from '@angular/router';
import {UserForLogin} from '../_models/UserForLogin';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  user: UserForLogin;
  loginForm: FormGroup;

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService,
              private fb: FormBuilder,
              private route: Router) { }

  ngOnInit(): void {
    this.createLoginForm();

    console.log(this.getUserDecodedToken());
  }

  createLoginForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern('[^@]+@[^@]+\\.[^@]+')],
       ],
      password: ['', [Validators.required]],
    });
  }

  logIn() {
    if (this.loginForm.valid) {
      this.user = Object.assign({}, this.loginForm.value);
      this.authService.logIn(this.user).subscribe(() => {
        this.alertifyService.success('Login successful');
      }, error => {
        this.alertifyService.error('Failed to log in');
      }, () => {
        this.authService.logIn(this.user).subscribe(() => {
          this.route.navigate(['/']);
        });
      });
    }
  }

  getUserDecodedToken() {
    return this.authService.getDecodedToken();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');

    this.alertifyService.message('Logged out!');
    this.route.navigate(['/']);
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}
