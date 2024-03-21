import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
  })
export class AppComService {
    tickleToggle = false;
    tickleToggleChanged = new Subject<boolean>();

    constructor() {}

    toggleTickler() {
        this.tickleToggle = !this.tickleToggle;
        this.tickleToggleChanged.next(this.tickleToggle);
    }
}