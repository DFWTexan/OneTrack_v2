import {
  Component,
  OnInit,
  Injectable,
  OnDestroy,
  ChangeDetectorRef,
} from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { AgentInfo, StockTickler } from '../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  EmployeeDataService,
  ModalService,
  TicklerMgmtComService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
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
  // selectedAgentData: number[] = [];
  ticklerCount: number = 0;
  worklistCount: number = 0;
  isShowLicenseMgmt: boolean = false;
  isShowTickle: boolean = false;
  typeTickler: any = {};

  stockTicklerItems: StockTickler[] = [];
  licenseTechItems: any = ['Loading...'];

  private subscriptions = new Subscription();

  constructor(
    private agentDataService: AgentDataService,
    public emplyService: EmployeeDataService,
    private route: ActivatedRoute,
    public agentComService: AgentComService,
    public appComService: AppComService,
    public ticklerMgmtComService: TicklerMgmtComService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    protected modalService: ModalService,
    public userAcctInfoDataService: UserAcctInfoDataService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isLoading = true;
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];

      // this.subscriptions.add(
      //   this.agentDataService
      //     .fetchAgentInformation(this.id)
      //     .subscribe((agentInfo: AgentInfo) => {
      //       this.isLoading = false;
      //       this.ticklerCount = agentInfo.ticklerItems.length;
      //       this.worklistCount = agentInfo.worklistItems.length;
      //       this.agentInfo = agentInfo;
      //     })
      // );
      if (this.appComService.isAllAgentsSelected) {
        this.currentIndex = this.appComService.selectAllAgentsIndex;
        this.getAgentInfo(this.appComService.selectAllAgents[this.currentIndex]);
      } else {
      this.getAgentInfo(this.id);
      }
    });

    this.agentInfo = this.agentDataService.agentInformation;
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: AgentInfo) => {
          this.isLoading = false;
          this.ticklerCount = agentInfo.ticklerItems.length;
          this.worklistCount = agentInfo.worklistItems.length;
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

    this.subscriptions.add(
      this.appComService.selectAllAgentsIndexChanged.subscribe(
        (index: number) => {
          this.currentIndex = index;
        }
      )
    );

    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchLicenseTech(0, null)
        .subscribe((licenseTechItems) => {
          this.licenseTechItems = licenseTechItems;
        })
    );

    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchStockTickler()
        .subscribe((stockTicklerItems) => {
          this.stockTicklerItems = stockTicklerItems;
        })
    );

    this.subscriptions.add(
      this.appComService.typeTicklerChanged.subscribe(
        (typeTickler: any) => {
          this.typeTickler = typeTickler;
          // if (this.typeTickler.type === 'LOA') {
          //   this.ticklerForm.get('lineOfAuthorityName')?.setValue(this.typeTickler.loa);
          // }
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

  onSetTypeTickler(typeTickler: any) {
    this.typeTickler = typeTickler;
  }

  private getAgentInfo(employeeId: number) {
    this.subscriptions.add(
      this.agentDataService
        .fetchAgentInformation(employeeId)
        .subscribe((agentInfo: AgentInfo) => {
          this.isLoading = false;
          this.ticklerCount = agentInfo.ticklerItems.length;
          this.worklistCount = agentInfo.worklistItems.length;
          this.agentInfo = agentInfo;
        })
    );
  }

  onChildCallRefreshData() {
    // this.subscriptions.add(
    //   this.agentDataService
    //     .fetchAgentInformation(this.id)
    //     .subscribe((agentInfo: AgentInfo) => {
    //       this.isLoading = false;
    //       this.ticklerCount = agentInfo.ticklerItems.length;
    //       this.worklistCount = agentInfo.worklistItems.length;
    //       this.agentInfo = agentInfo;
    //     })
    // );
    this.getAgentInfo(this.id);
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
      this.appComService.updateSelectAllAgentsIndex(this.currentIndex);
      this.getAgentInfo(this.appComService.selectAllAgents[this.currentIndex]);
    }
  }

  previousPage() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.appComService.updateSelectAllAgentsIndex(this.currentIndex);
      this.getAgentInfo(this.appComService.selectAllAgents[this.currentIndex]);
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
