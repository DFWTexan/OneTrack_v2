import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

import { AgentInfo, EmailComTemplate } from '../../../_Models';
import { AgentDataService, EmailDataService } from '../../../_services';

@Component({
  selector: 'app-tm-email',
  templateUrl: './tm-email.component.html',
  styleUrl: './tm-email.component.css',
})
@Injectable()
export class TmEmailComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  emailComTemplates: EmailComTemplate[] = [];
  selectedTemplate: number = 33;
  htmlContent: SafeHtml = '' as SafeHtml;
  subscribeAgentInfo: Subscription;
  subscribeEmailComTemplates: Subscription;

  ccEmail: string[] = [];
  chkMgr: boolean = false;
  chkDM: boolean = false;
  chkRD: boolean = false;

  constructor(
    private emailDataService: EmailDataService,
    public agentDataService: AgentDataService,
    private sanitizer: DomSanitizer
  ) {
    this.subscribeAgentInfo = new Subscription();
    this.subscribeEmailComTemplates = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: AgentInfo) => {
        this.agentInfo = agentInfo;

        this.emailDataService
          .fetchEmailComTemplateByID(33, this.agentInfo.employmentID)
          .subscribe((rawHtmlContent: string) => {

console.log('EMFTEST (app-tm-email) - template =>', rawHtmlContent);

            // this.htmlContent = rawHtmlContent;
            this.htmlContent = this.sanitizer.bypassSecurityTrustHtml(rawHtmlContent);
          });
      }
    );
    this.emailDataService
      .fetchEmailComTemplates()
      .subscribe((emailComTemplates: EmailComTemplate[]) => {
        this.emailComTemplates = emailComTemplates;
      });
  }

  onTemplateChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.emailDataService
      .fetchEmailComTemplateByID(+value, this.agentInfo.employmentID)
      .subscribe((template: string) => {
        this.htmlContent = template;
      });
  }

  onSubmit(form: NgForm) {
    console.log(form);
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
    this.subscribeEmailComTemplates.unsubscribe();
  }
}
