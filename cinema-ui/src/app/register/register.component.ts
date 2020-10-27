  import { Component, OnInit, Output, EventEmitter } from '@angular/core';
  import { AlertifyService } from '../_services/alertify.service';
  import { FormGroup, Validators, FormBuilder } from '@angular/forms';
  import { User } from '../_models/user';
  import { Router } from '@angular/router';
  import {AuthService} from '../_services/auth.service';


  @Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  user: User;
  constructor(private authService: AuthService, private alertifyService: AlertifyService, private fb: FormBuilder,
              private route: Router) { }
  registerForm: FormGroup;

  ngOnInit() {
    this.createRegisterForm();
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { 'missmatch': true};
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertifyService.success('Registration successful');
      }, error => {
        this.alertifyService.error(error);
      }, () => {
        this.authService.logIn(this.user).subscribe(() => {
          this.route.navigate(['/']);
        });
      });
    }
  }
}
