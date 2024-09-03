import { Injectable } from '@angular/core';
import {
  catchError,
  Observable,
  Subject,
  Subscription,
  throwError,
} from 'rxjs';

import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ErrorMessageService } from '../error/error.message.service';

@Injectable({
  providedIn: 'root',
})
export class AppComService {
  environment = environment;
  isLoggedIn = false;
  isLoggedInChanged = new Subject<boolean>();
  loginErrorMsg = '';
  loginErrorMsgChanged = new Subject<string>();
  isShowEditID = environment.isShowEditID;
  tickleToggle = false;
  tickleToggleChanged = new Subject<boolean>();
  isLegacyView = false;
  isLegacyViewChanged = new Subject<boolean>();

  subscriptions: Subscription = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private http: HttpClient
  ) {}

  toggleTickler() {
    this.tickleToggle = !this.tickleToggle;
    this.tickleToggleChanged.next(this.tickleToggle);
  }

  updateIsLoggedIn(isLoggedIn: boolean) {
    this.isLoggedIn = isLoggedIn;
    this.isLoggedInChanged.next(this.isLoggedIn);
  }

  updateEnvironment(value: string) {

  }

  updateLoginErrorMsg(loginErrorMsg: string) {
    this.loginErrorMsg = loginErrorMsg;
    this.loginErrorMsgChanged.next(this.loginErrorMsg);
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
