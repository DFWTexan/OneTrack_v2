import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AgentInfo, DiaryItem } from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ModalService,
  PaginationComService,
} from '../../../../_services';

@Component({
  selector: 'app-tm-diary',
  templateUrl: './tm-diary.component.html',
  styleUrl: './tm-diary.component.css',
})
@Injectable()
export class TmDiaryComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  agentInfo: AgentInfo = {} as AgentInfo;
  diaryItems: DiaryItem[] = [];
  
  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public paginationComService: PaginationComService
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.agentInfo = agentInfo;
      })
    );

    this.subscriptions.add(
      this.agentDataService.diaryItemsChanged.subscribe((diaryData: any) => {
        this.diaryItems = diaryData;
        this.paginationComService.updateDisplayItems(this.diaryItems);
        this.paginationComService.updatePaginatedResults();
        this.isLoading = false;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
