import { Component, OnInit, HostListener } from '@angular/core';
import { UserService } from '../../core/services/user.service';
import { Router } from '../../../../node_modules/@angular/router';
import { AlertService } from '../../core/services/alert.service';
import { FormBuilder, Validators, FormGroup } from '../../../../node_modules/@angular/forms';
import { IconDefinition } from '../../../../node_modules/@fortawesome/fontawesome-svg-core';
import { faUser, faKey } from '../../../../node_modules/@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public form: FormGroup= null;
  public isLogggingIn: boolean = false;
  public icons: { [key: string]: IconDefinition } = {
    "faUser": faUser,
    "faKey": faKey
  }

  constructor(
    private userService: UserService,
    private alertService: AlertService,
    private router: Router,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  public handleEnter(event: KeyboardEvent) {
    this.Login();
  }

  public Login() {
    const username = this.form.value.username;
    const password = this.form.value.password;

    if(!username || !password) {
      this.alertService.ShowError('Invalid username or password');
      return;
    }

    this.isLogggingIn = true;
    this.userService.Login(username, password)
    .subscribe((result) => {
      this.router.navigate(['/dashboard']);
      this.alertService.ShowSuccess('Successfully logged in');
      this.isLogggingIn = false;
    }, (err) => {
      this.isLogggingIn = false;
      if(err.status === 400) {
        this.alertService.ShowError('Invalid username or password');
      } else {
        this.alertService.ShowError('Unexpected error');
      }
    });
  }

}
