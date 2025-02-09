import {
  Component,
  OnInit,
  Injectable,
  OnDestroy,
  ChangeDetectorRef,
} from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { AgentInfo } from '../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css',
})
@Injectable()
export class AgentInformationComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  currentIndex: number = 0;
  id: number = 0;
  agentInfo: AgentInfo = {} as AgentInfo;
  ticklerCount: number = 0;
  worklistCount: number = 0;
  isShowLicenseMgmt: boolean = false;
  isShowTickle: boolean = false;

  private subscriptions = new Subscription();

  constructor(
    private agentDataService: AgentDataService,
    private route: ActivatedRoute,
    public agentComService: AgentComService,
    public appComService: AppComService,
    protected modalService: ModalService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isLoading = true;
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];

      this.subscriptions.add(
        this.agentDataService
          .fetchAgentInformation(this.id)
          .subscribe((agentInfo: AgentInfo) => {
            this.isLoading = false;
            this.ticklerCount = agentInfo.ticklerItems.length;
            this.worklistCount = agentInfo.worklistItems.length;
            this.agentInfo = agentInfo;
          })
      );
    });

    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: AgentInfo) => {
          this.isLoading = false;
          this.agentInfo = agentInfo;
        }
      )
    );

    this.subscriptions.add(
      this.agentComService.isShowLicenseMgmtChanged.subscribe(
        (showLicenseMgmt: boolean) => {
          this.isShowLicenseMgmt = showLicenseMgmt;
        }
      )
    );

    this.subscriptions.add(
      this.appComService.tickleToggleChanged.subscribe(
        (tickleToggle: boolean) => {
          this.isShowTickle = tickleToggle;
        }
      )
    );
  }

  // viewLicenseMgmt() {
  //   this.agentComService.showLicenseMgmt();
  //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
  //     this.router.navigate(['team/agent-info', this.id, 'tm-license-mgmt']);
  //   });
  // }

  onChildCallRefreshData() {
    this.subscriptions.add(
      this.agentDataService
        .fetchAgentInformation(this.id)
        .subscribe((agentInfo: AgentInfo) => {
          this.isLoading = false;
          this.ticklerCount = agentInfo.ticklerItems.length;
          this.worklistCount = agentInfo.worklistItems.length;
          this.agentInfo = agentInfo;
        })
    );
  }

  exitLicenseMgmt() {
    this.agentComService.showLicenseMgmt();
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['../../team/agent-info', this.id, 'tm-info-mgmt']);
    });
  }

  // Pagination
  nextPage() {
    if (this.currentIndex < this.appComService.selectAllAgents.length - 1) {
      this.currentIndex++;
      // this.agentDataService.updateLicenseMgmtDataIndex(this.currentIndex);
    }
  }

  previousPage() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      // this.agentDataService.updateLicenseMgmtDataIndex(this.currentIndex);
    }
  }

  getPageInfo(): string {
    return `${this.currentIndex + 1} of ${this.appComService.selectAllAgents.length}`;
  }

  isDisplayPrevious(): boolean {
    return this.currentIndex > 0;
  }

  isDisplayNext(): boolean {
    return this.currentIndex < this.appComService.selectAllAgents.length - 1;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
