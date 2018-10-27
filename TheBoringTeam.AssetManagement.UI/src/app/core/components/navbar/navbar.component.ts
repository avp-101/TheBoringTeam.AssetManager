import { Component, OnInit, OnDestroy } from '@angular/core';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faParachuteBox, faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { UserService } from '../../services/user.service';
import { Subscription } from '../../../../../node_modules/rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {

  private userChangeSubscription: Subscription;
  public isLoggedIn: boolean = false;
  public user: any = null;

  public icons: { [key: string]: IconDefinition } = {
    "faParachuteBox": faParachuteBox,
    "faUserCircle": faUserCircle
  } 

  constructor(
    private readonly userService: UserService
  ) { }

  ngOnDestroy() {
    this.userChangeSubscription.unsubscribe();
  }

  ngOnInit() {
    this.userChangeSubscription = this.userService.userChange.subscribe(
      (result) => {
        if(result) {
          this.user = result;
          this.isLoggedIn = true;
        } else {
          this.user = null;
          this.isLoggedIn = false;
        }
      }
    )
  }

  public Logout() {
    this.userService.Logout();
  }

}
