import { Component, OnInit, Injectable } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { AgentInfo } from '../../_Models';
import { AgentDataService } from '../../_services';
@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css',
})
@Injectable()
export class AgentInformationComponent implements OnInit{
  id: number = 0;
  agentInfo: AgentInfo = {} as AgentInfo;  

  constructor(private agentService: AgentDataService, 
              private route: ActivatedRoute, 
              private router: Router) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
      this.agentService.fetchAgentInformation(this.id)
        .subscribe((agentInfo: AgentInfo) => {
          this.agentInfo = agentInfo;
        });
    });
  }
}
