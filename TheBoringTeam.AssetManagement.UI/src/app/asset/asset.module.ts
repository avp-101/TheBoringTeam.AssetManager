import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssetListComponent } from './asset-list/asset-list.component';
import { AssetEditorComponent } from './asset-editor/asset-editor.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [AssetListComponent, AssetEditorComponent]
})
export class AssetModule { }
