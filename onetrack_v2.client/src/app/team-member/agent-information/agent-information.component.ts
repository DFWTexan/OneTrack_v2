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
} from '../../_services';

@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css',
})
@Injectable()
export class AgentInformationComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  id: number = 0;
  agentInfo: AgentInfo = {} as AgentInfo;
  isShowLicenseMgmt: boolean = false;
  subscribeShowLicMgmtChanged: Subscription = new Subscription();
  isShowTickle: boolean = false;
  subsctibeAgentInfo: Subscription = new Subscription();
  subscribeTickleToggleChanged: Subscription = new Subscription();
  subscibeAgentInfoChanged: Subscription = new Subscription();

  constructor(
    private agentDataService: AgentDataService,
    private route: ActivatedRoute,
    private agentComService: AgentComService,
    private appComService: AppComService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isLoading = true;
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
      this.subsctibeAgentInfo = this.agentDataService
        .fetchAgentInformation(this.id)
        .subscribe((agentInfo: AgentInfo) => {
          this.isLoading = false;
          this.agentInfo = agentInfo;
        });
    });

    this.subscribeShowLicMgmtChanged =
      this.agentComService.isShowLicenseMgmtChanged.subscribe(
        (showLicenseMgmt: boolean) => {
          this.isShowLicenseMgmt = showLicenseMgmt;
        }
      );

    this.subscribeTickleToggleChanged =
      this.appComService.tickleToggleChanged.subscribe(
        (tickleToggle: boolean) => {
          this.isShowTickle = tickleToggle;
        }
      );
  }

  // viewLicenseMgmt() {
  //   this.agentComService.showLicenseMgmt();
  //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
  //     this.router.navigate(['team/agent-info', this.id, 'tm-license-mgmt']);
  //   });
  // }

  exitLicenseMgmt() {
    this.agentComService.showLicenseMgmt();
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['team/agent-info', this.id, 'tm-info']);
    });
  }

  ngOnDestroy(): void {
    this.subsctibeAgentInfo.unsubscribe();
    this.subscribeShowLicMgmtChanged.unsubscribe();
    this.subscribeTickleToggleChanged.unsubscribe();
    this.subscibeAgentInfoChanged.unsubscribe();
  }
}
