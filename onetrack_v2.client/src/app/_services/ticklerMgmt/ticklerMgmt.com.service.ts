import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TicklerMgmtComService {
    modeTicklerMgmt: string = '';
    modeTicklerMgmtChanged = new Subject<string>();

    constructor() {}

    modeTicklerMgmtModal(mode: string) {
        this.modeTicklerMgmt = mode;
        this.modeTicklerMgmtChanged.next(this.modeTicklerMgmt);
    }
}