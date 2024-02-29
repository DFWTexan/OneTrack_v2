import { Component, OnInit, Injectable } from '@angular/core';

import { AgentInfo } from '../../_Models';
import { AgentDataService } from '../../_services';

@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css',
})
@Injectable()
export class AgentInformationComponent implements OnInit{
  agentInfo: AgentInfo = {} as AgentInfo;

  constructor(private agentService: AgentDataService) {}

  ngOnInit(): void {
    
  }
}
