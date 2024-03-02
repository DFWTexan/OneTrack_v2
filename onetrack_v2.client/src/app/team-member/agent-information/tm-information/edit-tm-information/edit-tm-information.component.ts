import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AgentDataService } from '../../../../_services';
import { AgentInfo } from '../../../../_Models';

@Component({
  selector: 'app-edit-tm-information',
  templateUrl: './edit-tm-information.component.html',
  styleUrl: './edit-tm-information.component.css',
})
@Injectable()
export class EditTmInformationComponent implements OnInit, OnDestroy {
  form = new FormGroup({
    employerAgency: new FormControl(''),
    preferredName: new FormControl(''),
    teamMemberID: new FormControl(''),
    nationalProducerNbr: new FormControl(''),
    agentStatus: new FormControl(''),
    residentSate: new FormControl(''),
    requiresContEdu: new FormControl(''),
    excludeFromReports: new FormControl(''),
  });

  agentInfo: AgentInfo = {} as AgentInfo;
  subscribeAgentInfo: Subscription;

  constructor(private agentService: AgentDataService) {
    this.subscribeAgentInfo = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
      }
    );
  }

  onSubmit() {
    console.log(this.form.value);
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
function injectable(): (
  target: typeof EditTmInformationComponent
) => void | typeof EditTmInformationComponent {
  throw new Error('Function not implemented.');
}
