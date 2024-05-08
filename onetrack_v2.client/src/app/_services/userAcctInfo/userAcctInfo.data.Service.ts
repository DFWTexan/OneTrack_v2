import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { UserAcctInfo } from '../../_Models';
import { AppComService } from '../common/app.com.service';

@Injectable({
  providedIn: 'root',
})
export class UserAcctInfoDataService {
  private apiUrl: string = environment.apiUrl + 'UserAcct/';
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  userAcctInfoChanged = new Subject<UserAcctInfo>();

  constructor(private http: HttpClient,
    private appComService: AppComService
  ) {}

  fetchUserAcctInfo(
    userName: string,
    passWord: string
  ): Observable<void | UserAcctInfo> {
    const queryParams = `?UserName=${userName}&PassWord=${passWord}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetUserAcctInfo${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.userAcctInfo = response.objData;
            this.userAcctInfoChanged.next(this.userAcctInfo);
            this.appComService.updateIsLoggedIn(true);
            return this.userAcctInfo;
          } else {
            this.appComService.updateIsLoggedIn(false);
            this.appComService.updateLoginErrorMsg(response.errMessage);
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}
