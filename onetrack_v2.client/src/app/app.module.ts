import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SideMenuNavComponent } from './_components';
import { ModalComponent } from './_components';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { CompanyEditComponent } from './admin/company-edit/company-edit.component';
import { CompanyRequirementsComponent } from './admin/company-requirements/company-requirements.component';
import { ContinueEducationEditComponent } from './admin/continue-education-edit/continue-education-edit.component';
import { DropdownListEditComponent } from './admin/dropdown-list-edit/dropdown-list-edit.component';
import { ExamEditComponent } from './admin/exam-edit/exam-edit.component';
import { JobTitleLicenseComponent } from './admin/job-title-license/job-title-license.component';
import { LicenseEditComponent } from './admin/license-edit/license-edit.component';
import { LicenseTechEditComponent } from './admin/license-tech-edit/license-tech-edit.component';
import { PreEducationEditComponent } from './admin/pre-education-edit/pre-education-edit.component';
import { ProductEditComponent } from './admin/product-edit/product-edit.component';
import { StateLicenseRequirementsComponent } from './admin/state-license-requirements/state-license-requirements.component';
import { StateProvinceEditComponent } from './admin/state-province-edit/state-province-edit.component';
import { XborLicRequirementsComponent } from './admin/xbor-lic-requirements/xbor-lic-requirements.component';
import { AddTeamMemberComponent } from './team-member/add-team-member/add-team-member.component';
import { SearchTeamMemberComponent } from './team-member/search-team-member/search-team-member.component';
import { ReportsComponent } from './reports/reports/reports.component';
import { WorkListComponent } from './work-list/work-list/work-list.component';
import { AgentInformationComponent } from './team-member/agent-information/agent-information.component';
import { ConstantsDataService } from './_services/';
import { SearchListComponent } from './team-member/search-team-member/search-list/search-list.component';
import { EmfTestPageComponent } from './emf-test/emf-test-page.component';
import { TmInformationComponent } from './team-member/agent-information/tm-information/tm-information.component';
import { TmDetailComponent } from './team-member/agent-information/tm-detail/tm-detail.component';
import { TmEmptransHistoryComponent } from './team-member/agent-information/tm-emptrans-history/tm-emptrans-history.component';
import { TmContinuingEduComponent } from './team-member/agent-information/tm-continuing-edu/tm-continuing-edu.component';
import { TmDiaryComponent } from './team-member/agent-information/tm-diary/tm-diary.component';
import { TmEmailComponent } from './team-member/agent-information/tm-email/tm-email.component';
import { TmCommunicationsComponent } from './team-member/agent-information/tm-communications/tm-communications.component';
import { EditTmInformationComponent } from './team-member/agent-information/tm-information/edit-tm-information/edit-tm-information.component';
import { EditLicenseAppointmentComponent } from './team-member/agent-information/tm-information/edit-license-appointment/edit-license-appointment.component';
import { TmLicenseManagementComponent } from './team-member/agent-information/tm-license-management/tm-license-management.component';
import { LicenseInfoComponent } from './team-member/agent-information/tm-license-management/license-info/license-info.component';
import { LicenseApplicationComponent } from './team-member/agent-information/tm-license-management/license-application/license-application.component';
import { LicenseRenewalComponent } from './team-member/agent-information/tm-license-management/license-renewal/license-renewal.component';
import { LicenseIncentiveComponent } from './team-member/agent-information/tm-license-management/license-incentive/license-incentive.component';
import { EditLicenseInfoComponent } from './team-member/agent-information/tm-license-management/license-info/edit-license-info/edit-license-info.component';
import { EditTmDetailComponent } from './team-member/agent-information/tm-detail/edit-tm-detail/edit-tm-detail.component';
import { PhoneFormatPipe } from './_shared/';
import { EditEmploymentHistComponent } from './team-member/agent-information/tm-emptrans-history/edit-employment-hist/edit-employment-hist.component';
import { EditTransferHistComponent } from './team-member/agent-information/tm-emptrans-history/edit-transfer-hist/edit-transfer-hist.component';

@NgModule({
  declarations: [
    AppComponent,
    ModalComponent,
    SideMenuNavComponent,
    DashboardComponent,
    CompanyEditComponent,
    CompanyRequirementsComponent,
    ContinueEducationEditComponent,
    DropdownListEditComponent,
    ExamEditComponent,
    JobTitleLicenseComponent,
    LicenseEditComponent,
    LicenseTechEditComponent,
    PreEducationEditComponent,
    ProductEditComponent,
    StateLicenseRequirementsComponent,
    StateProvinceEditComponent,
    XborLicRequirementsComponent,
    AddTeamMemberComponent,
    SearchTeamMemberComponent,
    ReportsComponent,
    WorkListComponent,
    AgentInformationComponent,
    SearchListComponent,
    EmfTestPageComponent,
    TmInformationComponent,
    TmDetailComponent,
    TmEmptransHistoryComponent,
    TmContinuingEduComponent,
    TmDiaryComponent,
    TmEmailComponent,
    TmCommunicationsComponent,
    EditTmInformationComponent,
    EditLicenseAppointmentComponent,
    TmLicenseManagementComponent,
    LicenseInfoComponent,
    LicenseApplicationComponent,
    LicenseRenewalComponent,
    LicenseIncentiveComponent,
    EditLicenseInfoComponent,
    EditTmDetailComponent,
    PhoneFormatPipe,
    EditEmploymentHistComponent,
    EditTransferHistComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSidenavModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    RouterModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatTabsModule,
    MatSelectModule,
    MatTooltipModule,
    RouterModule.forRoot([]),
  ],
  providers: [provideAnimationsAsync(), ConstantsDataService],
  bootstrap: [AppComponent],
})
export class AppModule {}
