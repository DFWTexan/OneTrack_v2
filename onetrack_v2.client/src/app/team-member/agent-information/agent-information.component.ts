import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { AgentInfo } from '../../_Models';
import { AgentComService, AgentDataService } from '../../_services';

@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css',
})
@Injectable()
export class AgentInformationComponent implements OnInit, OnDestroy{
  id: number = 0;
  agentInfo: AgentInfo = {} as AgentInfo;  
  isShowLicenseMgmt: boolean = false;
  subscribeShowLicMgmtChanged: Subscription = new Subscription();

  constructor(private agentDataService: AgentDataService, 
              private route: ActivatedRoute, 
              private agentComService: AgentComService) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
      this.agentDataService.fetchAgentInformation(this.id)
        .subscribe((agentInfo: AgentInfo) => {
          this.agentInfo = agentInfo;
        });
    });

    this.subscribeShowLicMgmtChanged = this.agentComService.isShowLicenseMgmtChanged.subscribe(
      (showLicenseMgmt: boolean) => {
        this.isShowLicenseMgmt = showLicenseMgmt;
      }
    );
  }

  ngOnDestroy(): void {
    this.subscribeShowLicMgmtChanged.unsubscribe();
  }
}
