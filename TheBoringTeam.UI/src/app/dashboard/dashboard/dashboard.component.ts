import { Component, OnInit } from '@angular/core';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faBoxes, faShieldAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public icons: { [key: string]: IconDefinition } = {
    "faBoxes": faBoxes,
    "faShieldAlt": faShieldAlt
  }

  constructor() { }

  ngOnInit() {
  }

}
