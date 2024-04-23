import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root',
  })
export class AppComService {
    isShowEditID = environment.isShowEditID;
    tickleToggle = false;
    tickleToggleChanged = new Subject<boolean>();

    constructor() {}

    toggleTickler() {
        this.tickleToggle = !this.tickleToggle;
        this.tickleToggleChanged.next(this.tickleToggle);
    }
}