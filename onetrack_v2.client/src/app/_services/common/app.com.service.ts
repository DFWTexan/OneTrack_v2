import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root',
  })
export class AppComService {
    isLoggedIn = false;
    isLoggedInChanged = new Subject<boolean>();
    loginErrorMsg = '';
    loginErrorMsgChanged = new Subject<string>();
    isShowEditID = environment.isShowEditID;
    tickleToggle = false;
    tickleToggleChanged = new Subject<boolean>();
    isLegacyView = false;
    isLegacyViewChanged = new Subject<boolean>();
    
    constructor() {}

    toggleTickler() {
        this.tickleToggle = !this.tickleToggle;
        this.tickleToggleChanged.next(this.tickleToggle);
    }

    updateIsLoggedIn(isLoggedIn: boolean) {
        this.isLoggedIn = isLoggedIn;
        this.isLoggedInChanged.next(this.isLoggedIn);
    }

    updateLoginErrorMsg(loginErrorMsg: string) {
        this.loginErrorMsg = loginErrorMsg;
        this.loginErrorMsgChanged.next(this.loginErrorMsg);
    }

    updateIsLegacyView(isLegacyView: boolean) {
        this.isLegacyView = isLegacyView;
        this.isLegacyViewChanged.next(this.isLegacyView);
    }
}