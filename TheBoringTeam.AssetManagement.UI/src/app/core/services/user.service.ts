import { Injectable } from '@angular/core';
import { HttpClient } from '../../../../node_modules/@angular/common/http';
import { environment } from '../../../environments/environment';
import { BehaviorSubject } from '../../../../node_modules/rxjs';
import { skip } from 'rxjs/operators'
import { Router } from '../../../../node_modules/@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private user$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  
  public get userChange() {
    return this.user$.asObservable();
  }
  public isLoggedIn: boolean = false;
  public accessToken: string = null;
  public user: any = null;
  
  constructor(
    private httpClient: HttpClient,
    private router: Router
  ) { }

  public Login(username: string, password: string) {
    var obs = new BehaviorSubject(null);
    this.httpClient
    .post(environment.apiUrl + '/api/user/authenticate', { username: username, password: password })
    .subscribe((result: any) => {
      this.accessToken = result.accessToken;
      this.isLoggedIn = true;
      this.user = result;
      this.user$.next(result);
      obs.next(result);
      obs.complete();
    }, (err) => {
      obs.error(err);
      obs.complete();
    });

    return obs.pipe(
      skip(1)
    );
  }

  public Logout() {
    this.accessToken = null;
    this.isLoggedIn = false;
    this.user = null;
    this.user$.next(null);
    this.router.navigate(['/login']);
  }
}
