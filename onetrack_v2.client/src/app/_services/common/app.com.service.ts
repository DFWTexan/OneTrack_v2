import { Injectable } from '@angular/core';
import {
  catchError,
  Observable,
  Subject,
  Subscription,
  throwError,
} from 'rxjs';

import { environment } from '../../_environments/environment';
import { HttpClient } from '@angular/common/http';
import { ErrorMessageService } from '../error/error.message.service';
import { ConfigService } from '../config/config.service';
import { EmployeeSearchResult, SearchEmployeeFilter, TechWorklistData } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class AppComService {
  config: any;
  isLoggedIn: boolean = false;
  isLoggedInChanged = new Subject<boolean>();
  loginErrorMsg = '';
  loginErrorMsgChanged = new Subject<string>();
  isShowEditID = false;
  tickleToggle = false;
  tickleToggleChanged = new Subject<boolean>();
  isLegacyView = false;
  isLegacyViewChanged = new Subject<boolean>();
  openTicklerCount = 0;
  openTicklerCountChanged = new Subject<number>();
  isOpenTicklerInfo = true;
  isOpenTicklerInfoChanged = new Subject<boolean>();
  openTechWorklistCount = 0;
  openTechWorklistCountChanged = new Subject<number>();
  techWorklistItems: TechWorklistData[] = [];
  techWorklistItemsChanged = new Subject<TechWorklistData[]>();
  techTicklerItems: any[] = [];
  techTicklerItemsChanged = new Subject<any[]>();
  selectAllAgents: any[] = [];
  // selectAllAgentsChanged = new Subject<any[]>();
  searchEmployeeFilter: SearchEmployeeFilter = {
    EmployeeSSN: null,
    TeamMemberGEID: null,
    NationalProducerNumber: 0,
    LastName: null,
    FirstName: null,
    ResState: null,
    WrkState: null,
    BranchCode: null,
    AgentStatus: ['All'],
    ScoreNumber: null,
    CompanyID: null,
    LicStatus: ['All'],
    LicState: null,
    LicenseName: null,
  };
  // searchEmployeeFilterChanged = new Subject<SearchEmployeeFilter>();
  searchEmployeeResult: EmployeeSearchResult[] =[];
  searchEmployeeResultChanged = new Subject<EmployeeSearchResult[]>();

  subscriptions: Subscription = new Subscription();

  constructor(
    private configService: ConfigService,
    public errorMessageService: ErrorMessageService,
    private http: HttpClient
  ) {
    this.initializeConfig();
  }

  async initializeConfig() {
    this.config = await this.configService.getConfig();
    if (this.config) {
      this.isLoggedIn = this.config.isDevLoginEnabled;
      this.isLoggedInChanged.next(this.config.isDevLoginEnabled);
      this.isShowEditID = this.config.isShowEditID;
    }
  }

  updateSearchEmployeeFilter(searchEmployeeFilter: SearchEmployeeFilter) {
    this.searchEmployeeFilter = searchEmployeeFilter;
    // this.searchEmployeeFilterChanged.next(this.searchEmployeeFilter);
  }

  updateSelectAllAgents(selectAllAgents: any[]) {
    this.selectAllAgents = selectAllAgents;
    // this.selectAllAgentsChanged.next(this.selectAllAgents);
  }

  updateSearchEmployeeResult(searchEmployeeResult: EmployeeSearchResult[]) {
    this.searchEmployeeResult = searchEmployeeResult;
    this.searchEmployeeResultChanged.next(this.searchEmployeeResult);
  }

  updateOpenTicklerCount(openTicklerCount: number) {
    this.openTicklerCount = openTicklerCount;
    this.openTicklerCountChanged.next(this.openTicklerCount);
  }

  updateTechWorklistItems(techWorklistItems: TechWorklistData[]) {
    this.techWorklistItems = techWorklistItems;
    this.techWorklistItemsChanged.next(this.techWorklistItems);
  }

  updateOpenTechWorklistCount(openTechWrkListCount: number) {
    this.openTechWorklistCount = openTechWrkListCount;
    this.openTechWorklistCountChanged.next(this.openTechWorklistCount);
  }

  updateTechTicklerItems(techTicklerItems: any[]) {
    this.techTicklerItems = techTicklerItems;
    this.techTicklerItemsChanged.next(this.techTicklerItems);
  }

  updateIsLoggedIn(isLoggedIn: boolean) {
    this.isLoggedIn = isLoggedIn;
    this.isLoggedInChanged.next(isLoggedIn);
  }

  updateIsOpenTicklerInfo(isOpenTicklerInfo: boolean) {
    this.isOpenTicklerInfo = isOpenTicklerInfo;
    this.isOpenTicklerInfoChanged.next(this.isOpenTicklerInfo);
  }

  updateLoginErrorMsg(loginErrorMsg: string) {
    this.loginErrorMsg = loginErrorMsg;
    this.loginErrorMsgChanged.next(this.loginErrorMsg);

    // Clear the login error message after one minute (30000 milliseconds)
    setTimeout(() => {
      this.loginErrorMsg = '';
      this.loginErrorMsgChanged.next(this.loginErrorMsg);
    }, 30000);
  }

  updateIsLegacyView(isLegacyView: boolean) {
    this.isLegacyView = isLegacyView;
    this.isLegacyViewChanged.next(this.isLegacyView);
  }

  openDocument(url: string): Observable<Blob> {
    return this.http
      .get(url, { responseType: 'blob' })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: any) {
    return throwError(
      () => new Error('Failed to load document - please try again later.')
    );
  }
}
