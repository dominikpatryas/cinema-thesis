import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.scss']
})
export class AboutUsComponent implements OnInit {
  public aboutUsSections = ['Our cinema', 'FAQ'];
  public currentSection = this.aboutUsSections[0];
  public facebookUrl = '';
  public twitterUrl = '';

  ngOnInit(): void {
  }

  public setCurrentSection(section: string): void {
    this.currentSection = section;
  }

}
