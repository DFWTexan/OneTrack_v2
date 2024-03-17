import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AgentInfo, DiaryItem } from '../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ModalService,
} from '../../../_services';

@Component({
  selector: 'app-tm-diary',
  templateUrl: './tm-diary.component.html',
  styleUrl: './tm-diary.component.css',
})
@Injectable()
export class TmDiaryComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  diaryItems: DiaryItem[] = [];
  subscribeAgentInfo: Subscription;
  subscribeDiaryData: Subscription;

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService
  ) {
    this.subscribeAgentInfo = new Subscription();
    this.subscribeDiaryData = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
      }
    );
    this.subscribeDiaryData = this.agentDataService.diaryItemsChanged.subscribe(
      (diaryData: any) => {
        this.diaryItems = diaryData;
      }
    );
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
    this.subscribeDiaryData.unsubscribe();
  }
}
