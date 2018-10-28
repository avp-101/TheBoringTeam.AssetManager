import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { LoginComponent } from './user/login/login.component';
import { IsLoggedGuard } from './core/guards/is-logged.guard';
import { IsNotLoggedGuard } from './core/guards/is-not-logged.guard';
import { ErrorComponent } from './core/components/error/error.component';
import { AssetListComponent } from './asset/asset-list/asset-list.component';
import { AssetEditorComponent } from './asset/asset-editor/asset-editor.component';
import { HasRightGuard } from './core/guards/has-right.guard';

var routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent, canActivate: [IsLoggedGuard] },
    { path: 'login', component: LoginComponent, canActivate: [IsNotLoggedGuard] },
    { path: 'asset', children: [
      { path: '', component: AssetListComponent, canActivate: [HasRightGuard], data: { right: 'AssetRead' } },
      { path: 'edit/:assetId', component: AssetEditorComponent, canActivate: [HasRightGuard], data: { right: 'AssetEdit' } },
      { path: 'create', component: AssetEditorComponent, canActivate: [HasRightGuard], data: { right: 'AssetEdit' } }
    ]},
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