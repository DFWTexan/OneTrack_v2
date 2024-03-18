import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import { EmailComTemplate } from '../../../_Models';
import { EmailDataService } from '../../../_services';

@Component({
  selector: 'app-tm-email',
  templateUrl: './tm-email.component.html',
  styleUrl: './tm-email.component.css',
})
@Injectable()
export class TmEmailComponent implements OnInit, OnDestroy {
  emailComTemplates: EmailComTemplate[] = [];
  htmlContent: string = '';
  subscribeEmailComTemplates: Subscription;

  constructor(private emailDataService: EmailDataService) {
    this.subscribeEmailComTemplates = new Subscription();
  }

  ngOnInit(): void {
    this.emailDataService
      .fetchEmailComTemplates()
      .subscribe((emailComTemplates: EmailComTemplate[]) => {
        this.emailComTemplates = emailComTemplates;
      });
  }

  onSubmit(form: NgForm) {
    console.log(form);
  }

  ngOnDestroy(): void {
    this.subscribeEmailComTemplates.unsubscribe();
  }
}
