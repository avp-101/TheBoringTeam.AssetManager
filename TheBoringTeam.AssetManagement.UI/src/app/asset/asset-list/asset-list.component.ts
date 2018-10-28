import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '../../../../node_modules/@angular/material';
import { AssetService } from '../../core/services/asset.service';

@Component({
  selector: 'app-asset-list',
  templateUrl: './asset-list.component.html',
  styleUrls: ['./asset-list.component.css']
})
export class AssetListComponent implements OnInit {

  isLoading = true;

  displayedColumns: string[] = ['identifier', 'tags'];
  dataSource = new MatTableDataSource<any>([]);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private assetService: AssetService
  ) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.assetService.Get().subscribe(
      (result: any) => {
        this.dataSource = new MatTableDataSource<any>(result);
        this.isLoading = false;
      }
    );
  }
}
