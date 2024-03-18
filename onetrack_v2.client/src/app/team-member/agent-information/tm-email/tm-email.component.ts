import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

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
  htmlContent: string = '';
  subscribeAgentInfo: Subscription;
  subscribeEmailComTemplates: Subscription;

  ccEmail: string[] = [];
  chkMgr: boolean = false;
  chkDM: boolean = false;
  chkRD: boolean = false;

  constructor(
    private emailDataService: EmailDataService,
    public agentDataService: AgentDataService
  ) {
    this.subscribeAgentInfo = new Subscription();
    this.subscribeEmailComTemplates = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: AgentInfo) => {
        this.agentInfo = agentInfo;
      }
    );
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
    this.subscribeAgentInfo.unsubscribe();
    this.subscribeEmailComTemplates.unsubscribe();
  }
}
