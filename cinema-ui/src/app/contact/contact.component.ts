import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  contactForm: FormGroup;
  contactModel: { email: string, message: string };
  sent = false;

  constructor(private builder: FormBuilder, private http: HttpClient) { }

  ngOnInit() {
    this.createContactForm();
  }

  createContactForm() {
    this.contactForm = this.builder.group({
      email: new FormControl('', [Validators.compose([Validators.required, Validators.email])]),
      message: new FormControl('', [Validators.required])
    });
  }

  onSubmit() {
    if (this.contactForm.valid) {
      this.contactModel = Object.assign({}, this.contactForm.value);

      this.http.post('mail_script.php', this.contactModel, {headers: { 'Content-type': 'text/plain' }}).subscribe((res) => {
        console.log('sent');
      }, e => {
        console.log('error');
      });

      this.contactForm.reset();
      this.sent = true;
    }
  }
}
