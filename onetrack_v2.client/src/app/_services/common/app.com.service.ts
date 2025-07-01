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
  isAllAgentsSelected = false;
  isAllAgentsSelectedChanged = new Subject<boolean>();
  selectAllAgents: number[] = [];
  selectAllAgentsChanged = new Subject<number[]>();
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
  searchEmployeeResult: EmployeeSearchResult[] =[];
  searchEmployeeResultChanged = new Subject<EmployeeSearchResult[]>();
  selectAllAgentsIndex: number = 0;
  selectAllAgentsIndexChanged = new Subject<number>();
  selectedEmploymentCommunicationID: number = 0;
  selectedEmploymentCommunicationIDChanged = new Subject<number>();
  typeTickler: any = {};
  typeTicklerChanged = new Subject<any>();
  appMessage: string = '';
  appMessageChanged = new Subject<string>();
  isShowModalIncentiveInfo: boolean = false;
  isShowModalIncentiveInfoChanged = new Subject<boolean>();
  indexInfoResetToggle: boolean = false;
  indexInfoResetToggleChanged = new Subject<boolean>();
  
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

  updateIndexInfoResetToggle() {
    this.indexInfoResetToggle = !this.indexInfoResetToggle;
    this.indexInfoResetToggleChanged.next(this.indexInfoResetToggle);
  }
  
  updateIsShowModalIncentiveInfo(isShowModalIncentiveInfo: boolean) {
    this.isShowModalIncentiveInfo = isShowModalIncentiveInfo;
    this.isShowModalIncentiveInfoChanged.next(this.isShowModalIncentiveInfo);
  }

  updateSearchEmployeeFilter(searchEmployeeFilter: SearchEmployeeFilter) {
    this.searchEmployeeFilter = searchEmployeeFilter;
  }

  updateSelectAllAgentsIndex(selectAllAgentsIndex: number) {
    this.selectAllAgentsIndex = selectAllAgentsIndex;
    this.selectAllAgentsIndexChanged.next(this.selectAllAgentsIndex);
  }

  updateIsAllAgentsSelected(isAllAgentsSelected: boolean) {
    this.isAllAgentsSelected = isAllAgentsSelected;
    this.isAllAgentsSelectedChanged.next(this.isAllAgentsSelected);
  }

  updateSelectAllAgents(selectAllAgents: number[]) {
    this.selectAllAgents = selectAllAgents;
    this.selectAllAgentsChanged.next(this.selectAllAgents);
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

  updateSelectedEmploymentCommunicationID(selectedEmploymentCommunicationID: number) {
    this.selectedEmploymentCommunicationID = selectedEmploymentCommunicationID;
    this.selectedEmploymentCommunicationIDChanged.next(this.selectedEmploymentCommunicationID);
  }

  updateTypeTickler(typeTickler: any) {
    this.typeTickler = typeTickler;
    this.typeTicklerChanged.next(this.typeTickler);
  }

  openDocument(url: string): Observable<Blob> {
    return this.http
      .get(url, { responseType: 'blob' })
      .pipe(catchError(this.handleError));
  }

  updateAppMessage(appMessage: string) {
    this.appMessage = appMessage;
    this.appMessageChanged.next(this.appMessage);
    // Clear the app message after 5 seconds
    setTimeout(() => {
      this.appMessage = '';
      this.appMessageChanged.next(this.appMessage);
    }, 5000);
  }

  private handleError(error: any) {
    return throwError(
      () => new Error('Failed to load document - please try again later.')
    );
  }
}
