import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { LoginComponent } from './user/login/login.component';
import { IsLoggedGuard } from './core/guards/is-logged.guard';
import { IsNotLoggedGuard } from './core/guards/is-not-logged.guard';
import { ErrorComponent } from './core/components/error/error.component';

var routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent, canActivate: [IsLoggedGuard] },
    { path: 'login', component: LoginComponent, canActivate: [IsNotLoggedGuard] },
    { path: 'error', component: ErrorComponent },
    { path: '**', redirectTo: '/error?code=3'}
]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
      RouterModule
  ]
})
export class AppRoutingModule { }