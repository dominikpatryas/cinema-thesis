import {Component, Input, OnInit} from '@angular/core';
import html2canvas from 'html2canvas';
import { jsPDF } from 'jspdf';
import { v4 as uuidv4 } from 'uuid';
import {User} from '../_models/User';
import {AuthService} from '../_services/auth.service';
import {TicketsService} from '../_services/tickets.service';
import {ActivatedRoute, Route, Router} from '@angular/router';
@Component({
  selector: 'app-ticket',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {
  ticket;

  constructor(private ticketService: TicketsService, private activatedRoute: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.ticketService.getTicket(this.activatedRoute.snapshot.paramMap.get('id')).subscribe((ticket) => {
      this.ticket = ticket;

    }, error => {
      this.router.navigate(['']);
    });
  }

  generatePDF() {
    const data = document.getElementById('contentToConvert');
    html2canvas(data).then(canvas => {
      const imgWidth = 208;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      const contentDataURL = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4');
      const position = 0;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
      pdf.save(`ticket-${uuidv4()}.pdf`);
    });
  }

}
