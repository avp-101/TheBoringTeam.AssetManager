import { Component, OnInit } from '@angular/core';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faBoxes, faShieldAlt } from '@fortawesome/free-solid-svg-icons';
import { UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public canSeeAssets: boolean = false;
  public canSeeSecurity: boolean = false;
  public icons: { [key: string]: IconDefinition } = {
    "faBoxes": faBoxes,
    "faShieldAlt": faShieldAlt
  }

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.canSeeAssets = this.userService.user.role.rights.filter(f => f.name === "AssetRead").length > 0;
    this.canSeeSecurity = this.userService.user.role.rights.filter(f => f.name === "UserRead").length > 0;
  }

}
