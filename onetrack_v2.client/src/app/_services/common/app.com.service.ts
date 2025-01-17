import { Injectable } from '@angular/core';
import {
  catchError,
  Observable,
  Subject,
  Subscription,
  throwError,
} from 'rxjs';

import { environment } from '../../_environments/environment';
import { HttpClient } from '@angular/common/http';
import { ErrorMessageService } from '../error/error.message.service';
import { ConfigService } from '../config/config.service';

@Injectable({
  providedIn: 'root',
})
export class AppComService {
  config: any;
  isLoggedIn: boolean = false;
  isLoggedInChanged = new Subject<boolean>();
  loginErrorMsg = '';
  loginErrorMsgChanged = new Subject<string>();
  isShowEditID = false;
  tickleToggle = false;
  tickleToggleChanged = new Subject<boolean>();
  isLegacyView = false;
  isLegacyViewChanged = new Subject<boolean>();
  openTicklerCount = 0;
  openTicklerCountChanged = new Subject<number>();
  isOpenTicklerInfo = true;
  isOpenTicklerInfoChanged = new Subject<boolean>();

  subscriptions: Subscription = new Subscription();

  constructor(
    private configService: ConfigService,
    public errorMessageService: ErrorMessageService,
    private http: HttpClient
  ) {
    this.initializeConfig();
  }

  async initializeConfig() {
    this.config = await this.configService.getConfig();
    if (this.config) {
      this.isLoggedIn = this.config.isDevLoginEnabled;
      this.isLoggedInChanged.next(this.config.isDevLoginEnabled);
      this.isShowEditID = this.config.isShowEditID;
    }
  }

  updateOpenTicklerCount(openTicklerCount: number) {
    this.openTicklerCount = openTicklerCount;
    this.openTicklerCountChanged.next(this.openTicklerCount);
  }

  updateIsLoggedIn(isLoggedIn: boolean) {
    this.isLoggedIn = isLoggedIn;
    this.isLoggedInChanged.next(isLoggedIn);
  }

  updateIsOpenTicklerInfo(isOpenTicklerInfo: boolean) {
    this.isOpenTicklerInfo = isOpenTicklerInfo;
    this.isOpenTicklerInfoChanged.next(this.isOpenTicklerInfo);
  }

  updateLoginErrorMsg(loginErrorMsg: string) {
    this.loginErrorMsg = loginErrorMsg;
    this.loginErrorMsgChanged.next(this.loginErrorMsg);

    // Clear the login error message after one minute (30000 milliseconds)
    setTimeout(() => {
      this.loginErrorMsg = '';
      this.loginErrorMsgChanged.next(this.loginErrorMsg);
    }, 30000);
  }

  updateIsLegacyView(isLegacyView: boolean) {
    this.isLegacyView = isLegacyView;
    this.isLegacyViewChanged.next(this.isLegacyView);
  }

  openDocument(url: string): Observable<Blob> {
    return this.http
      .get(url, { responseType: 'blob' })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: any) {
    return throwError(
      () => new Error('Failed to load document - please try again later.')
    );
  }
}
