import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';

import { AgentDataService } from '../../../_services';
import { Subscription } from 'rxjs';
import { AgentInfo } from '../../../_Models';
import { ModalService } from '../../../_services';

@Component({
  selector: 'app-tm-information',
  templateUrl: './tm-information.component.html',
  styleUrl: './tm-information.component.css',
})
@Injectable()
export class TmInformationComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  agentInfo: AgentInfo = {} as AgentInfo;
  bodyText = 'This text can be updated in modal 1';

  constructor(
    private agentService: AgentDataService,
    protected modalService: ModalService
  ) {
    this.subscription = new Subscription();
  }

  ngOnInit() {
    this.subscription = this.agentService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
      }
    );
  }

  openModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'block';
    }
  }
  closeModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
