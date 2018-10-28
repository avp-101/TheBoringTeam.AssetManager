import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { HttpClient } from '../../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AssetService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public Get() {
    return this.httpClient.get(environment.apiUrl + "/api/asset");
  }
}
