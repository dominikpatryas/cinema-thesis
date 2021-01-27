import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateService {
  days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  constructor() { }

  convertDatetoView(date: string) {
    const newDate = new Date(date);
    return `${newDate.toLocaleDateString()} ${this.days[newDate.getDay()]} ${newDate.toLocaleTimeString()}` ;
  }
}
