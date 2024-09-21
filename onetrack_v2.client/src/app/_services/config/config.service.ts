// src/app/services/config.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../_environments/environment';
import { firstValueFrom } from 'rxjs';
import { AppComService } from '../common/app.com.service';
import { UserAcctInfoDataService } from '../userAcctInfo/userAcctInfo.data.Service';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  public config: any;

  constructor(private http: HttpClient) {}

  async loadConfig() {
    this.config = await firstValueFrom(this.http.get(environment.configFile));
  }
  //   loadConfig() {
  //     return this.http.get(environment.configFile).toPromise().then((data) => {

  // console.log('EMFTEST (configService: loadConfig) data => \n', data);

  //       this.config = data;
  //     });
  //   }

  getConfig() {
    return this.config;
  }
}
