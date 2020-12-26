import { Component, OnInit } from '@angular/core';
import {HallsService} from '../../_services/halls.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AlertifyService} from '../../_services/alertify.service';

@Component({
  selector: 'app-hall',
  templateUrl: './add-hall.component.html',
  styleUrls: ['./add-hall.component.scss']
})
export class AddHallComponent implements OnInit {
  hallForm: FormGroup;
  constructor(private hallsService: HallsService, private fb: FormBuilder, private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.createHallForm();
  }

  createHallForm() {
    this.hallForm = this.fb.group({
      hallNumber: [null, Validators.required],
      seatNumbers: [null, Validators.required]
    });
  }

  addHall() {
    if (this.hallForm.valid) {
      const hall = Object.assign({}, this.hallForm.value);
      this.hallsService.addHall(hall).subscribe(() => {
        this.alertifyService.success('Added hall successfully.');
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }
}
