import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

import { AppComService, UserAcctInfoDataService } from '../../_services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  constructor(
    private userAcctInfoService: UserAcctInfoDataService,
    public appComService: AppComService
  ) {}

  onSubmit(form: NgForm) {

    console.log(
      'EMFTEST - (app-login): userTID - ',
      form.value.loginInfo.userTID,
    );
    console.log(
      'EMFTEST - (app-login): password - ',
      form.value.loginInfo.password
    );

    this.userAcctInfoService
      .fetchUserAcctInfo(
        form.value.loginInfo.userTID,
        form.value.loginInfo.password
      )
      .subscribe((response) => {
        console.log('EMFTEST - (app-login): response => \n', response);
      });
  }
}
