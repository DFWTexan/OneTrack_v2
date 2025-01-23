import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { environment } from '../../_environments/environment';
import { UserAcctInfo } from '../../_Models';
import { AppComService } from '../common/app.com.service';

@Injectable({
  providedIn: 'root',
})
export class UserAcctInfoDataService {
  private apiUrl: string = environment.apiUrl + 'UserAcct/';
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  userAcctInfoChanged = new Subject<UserAcctInfo>();
  userID: string = '';
  userPassWord: string = '';

  constructor(private http: HttpClient, private appComService: AppComService) {}

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
            // return this.userAcctInfo;
          } else {
            this.appComService.updateIsLoggedIn(false);
            this.appComService.updateLoginErrorMsg(response.errMessage);
          }
        }),
        catchError((error: any) => {
          this.appComService.updateIsLoggedIn(false);
          if (error.error && error.error.errMessage) {
            this.appComService.updateLoginErrorMsg(error.error.errMessage);
          } else {
            this.appComService.updateLoginErrorMsg('Unknown error');
          }

          return of(this.userAcctInfo);
        })
      );
  }

  fetchLicenseTechByID(licenseTechID: number): Observable<any> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetLicenseTechByID/=${licenseTechID}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData;
          } else {
            this.appComService.updateLoginErrorMsg(response.errMessage);
          }
        }),
        catchError((error: any) => {
          if (error.error && error.error.errMessage) {
            this.appComService.updateLoginErrorMsg(error.error.errMessage);
          } else {
            this.appComService.updateLoginErrorMsg('Unknown error');
          }

          return of(this.userAcctInfo);
        })
      );
  }

  fetchLicenseTechBySOEID(soeID: string): Observable<any> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetLicenseTechBySOEID/${soeID}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData;
          } else {
            this.appComService.updateLoginErrorMsg(response.errMessage);
          }
        }),
        catchError((error: any) => {
          if (error.error && error.error.errMessage) {
            this.appComService.updateLoginErrorMsg(error.error.errMessage);
          } else {
            this.appComService.updateLoginErrorMsg('Unknown error');
          }

          return of(this.userAcctInfo);
        })
      );
  }

  updateUserAcctInfo(userAcctInfo: UserAcctInfo) {
    this.userAcctInfo = userAcctInfo;
    this.userAcctInfoChanged.next(userAcctInfo);
  }

  storeUserCredentials(userId: string, passWord: string) {
    this.userID = userId;
    this.userPassWord = passWord;
  } 
}
