import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ErrorMessageService {
  errorMessage: string = '';
  errorMessageChanged = new Subject<string>();

  constructor() {}

  setErrorMessage(message: string) {
    this.errorMessage = message;
    this.errorMessageChanged.next(this.errorMessage);

    setTimeout(() => {
        this.clearErrorMessage();
      }, 60000);
  }

  clearErrorMessage() {
    this.errorMessage = '';
    this.errorMessageChanged.next(this.errorMessage);
  }
}
