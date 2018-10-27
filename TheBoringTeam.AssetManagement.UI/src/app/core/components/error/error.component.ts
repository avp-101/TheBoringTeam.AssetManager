import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

  public error;

  public errors = [
    { image: 'assets/broken.svg', title: 'Something went wrong...', message: 'Please refresh the browser. If you are still having this issue please contact local support.' },
    { image: 'assets/unauthorized.svg', title: 'Unauthorized!', message: 'Please login to continue.' },
    { image: 'assets/forbidden.svg', title: 'Forbidden!' , message: 'You are not allowed to access this resource.'},
    { image: 'assets/404.svg', title: '404!' , message: 'The page you are searching does not exist.' },
  ];

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    const code = this.activatedRoute.snapshot.queryParams['code'] || 0;
    this.error = this.errors[code] || this.errors[0];
  }

}
