import {
  Component,
  ViewChild,
  OnInit,
  Inject,
  Injectable,
} from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';

import { AppComService, UserAcctInfoDataService } from './_services';
import { environment } from './environments/environment';

@Injectable()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  // @ViewChild(MatSidenav)
  // sidenav!: MatSidenav;
  // isMobile = true;
  // isCollapsed = true;

  constructor(
    public appComService: AppComService,
    private userInfoService: UserAcctInfoDataService
  ) {}

  ngOnInit() {
    if (!environment.production) {
      this.appComService.updateIsLoggedIn(true);
      this.userInfoService.updateUserAcctInfo({
        displayName: 'Erish Faggett',
        soeid: 'T2229513',
        email: 'erish.faggett@omf.com',
        enabled: true,
        employeeId: '2229513',
        homeDirectory: '\\\\corp.fin\\users\\EVNAS_Users\\3\\T2229513',
        lastLogon: '2024-05-07T14:30:59.1941967Z',
        isAdminRole: true,
        isTechRole: null,
        isReadRole: true,
        isSuperUser: true,
      });
    }
  }

  title = 'onetrack_v2';
}
