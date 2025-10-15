import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { MatFormFieldModule } from '@angular/material/form-field';
import { SharedModule } from '../shared/shared.module';

import { TmInformationComponent } from '../../team-member/agent-information/tm-info-management/tm-information/tm-information.component';
import { TmDetailComponent } from '../../team-member/agent-information/tm-info-management/tm-detail/tm-detail.component';
import { TmEmptransHistoryComponent } from '../../team-member/agent-information/tm-info-management/tm-emptrans-history/tm-emptrans-history.component';
import { TmContinuingEduComponent } from '../../team-member/agent-information/tm-info-management/tm-continuing-edu/tm-continuing-edu.component';
import { TmDiaryComponent } from '../../team-member/agent-information/tm-info-management/tm-diary/tm-diary.component';
import { TmEmailComponent } from '../../team-member/agent-information/tm-info-management/tm-email/tm-email.component';
import { TmCommunicationsComponent } from '../../team-member/agent-information/tm-info-management/tm-communications/tm-communications.component';
import { EditTmInformationComponent } from '../../team-member/agent-information/tm-info-management/tm-information/edit-tm-information/edit-tm-information.component';
import { EditLicenseAppointmentComponent } from '../../team-member/agent-information/tm-info-management/tm-information/edit-license-appointment/edit-license-appointment.component';
import { TmLicenseManagementComponent } from '../../team-member/agent-information/tm-license-management/tm-license-management.component';

import { LicenseInfoComponent } from '../../team-member/agent-information/tm-license-management/license-info/license-info.component';
import { LicenseApplicationComponent } from '../../team-member/agent-information/tm-license-management/license-application/license-application.component';
import { LicenseRenewalComponent } from '../../team-member/agent-information/tm-license-management/license-renewal/license-renewal.component';
import { LicenseIncentiveComponent } from '../../team-member/agent-information/tm-license-management/license-incentive/license-incentive.component';
import { AddLicenseApptComponent } from '../../team-member/agent-information/tm-license-management/license-info/add-license-appt/add-license-appt.component';
import { EditLicenseInfoComponent } from '../../team-member/agent-information/tm-license-management/license-info/edit-license-info/edit-license-info.component';
import { EditTmDetailComponent } from '../../team-member/agent-information/tm-info-management/tm-detail/edit-tm-detail/edit-tm-detail.component';

import { EditEmploymentHistComponent } from '../../team-member/agent-information/tm-info-management/tm-emptrans-history/edit-employment-hist/edit-employment-hist.component';
import { EditTransferHistComponent } from '../../team-member/agent-information/tm-info-management/tm-emptrans-history/edit-transfer-hist/edit-transfer-hist.component';
import { EditCorequirementHistComponent } from '../../team-member/agent-information/tm-info-management/tm-emptrans-history/edit-corequirement-hist/edit-corequirement-hist.component';
import { EditJobtitleHistComponent } from '../../team-member/agent-information/tm-info-management/tm-emptrans-history/edit-jobtitle-hist/edit-jobtitle-hist.component';
import { EditLicApplInfoComponent } from '../../team-member/agent-information/tm-license-management/license-application/edit-lic-appl-info/edit-lic-appl-info.component';
import { EditLicPreEduComponent } from '../../team-member/agent-information/tm-license-management/license-application/edit-lic-pre-edu/edit-lic-pre-edu.component';
import { EditLicPreExamComponent } from '../../team-member/agent-information/tm-license-management/license-application/edit-lic-pre-exam/edit-lic-pre-exam.component';
import { EditLicenseRenewalComponent } from '../../team-member/agent-information/tm-license-management/license-renewal/edit-license-renewal/edit-license-renewal.component';
import { EditHoursTakenComponent } from '../../team-member/agent-information/tm-info-management/tm-continuing-edu/edit-hours-taken/edit-hours-taken.component';
import { EditDiaryEntryComponent } from '../../team-member/agent-information/tm-info-management/tm-diary/edit-diary-entry/edit-diary-entry.component';
import { TicklerInfoComponent } from '../../_components/tickler-mgmt/tickler-info/tickler-info.component';
import { TicklerSearchComponent } from '../../_components/tickler-mgmt/tickler-search/tickler-search.component';
import { EditTicklerInfoComponent } from '../../_components/tickler-mgmt/tickler-info/edit-tickler-info/edit-tickler-info.component';

import { AddTicklerComponent } from '../../team-member/search-team-member/add-tickler/add-tickler.component';
import { AddIndexerComponent } from '../../team-member/search-team-member/add-indexer/add-indexer.component';
import { InsertIncentiveLicenseComponent } from '../../team-member/agent-information/tm-info-management/tm-information/insert-incentive-license/insert-incentive-license.component';

import { AgentInformationComponent } from '../../team-member/agent-information/agent-information.component';
import { TeamMemberRoutingModule } from '../../team-member/team-member-routing.module';
import { TicklerMgmtComponent } from '../../_components/tickler-mgmt/tickler-mgmt.component';
import { AddTeamMemberComponent } from '../../team-member/add-team-member/add-team-member.component';
import { SearchTeamMemberComponent } from '../../team-member/search-team-member/search-team-member.component';
import { SearchListComponent } from '../../team-member/search-team-member/search-list/search-list.component';
import { TmInfoManagementComponent } from '../../team-member/agent-information/tm-info-management/tm-info-management.component';
import { InsertMemberLoaTicklerComponent } from '../../team-member/agent-information/tm-info-management/tm-information/insert-member-loa-tickler/insert-member-loa-tickler.component';
import { AgentTicklerInfoComponent } from '../../team-member/agent-information/agent-tickler-info/agent-tickler-info.component';
import { AgentWishlistItemsComponent } from '../../team-member/agent-information/agent-wishlist-items/agent-wishlist-items.component';
// import { IMaskModule } from 'angular-imask';

const routes: Routes = [
  // Define team member routes here
  // Example: { path: 'add-member', component: AddTeamMemberComponent }
  { path: 'add-member', component: AddTeamMemberComponent },
  { path: 'search-member', component: SearchTeamMemberComponent },
  { path: 'search-list', component: SearchListComponent },
  { path: 'info-management', component: TmInfoManagementComponent },
  { path: 'insert-loa-tickler', component: InsertMemberLoaTicklerComponent },
  { path: 'agent-tickler-info', component: AgentTicklerInfoComponent },
  { path: 'agent-wishlist-items', component: AgentWishlistItemsComponent },
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    TeamMemberRoutingModule,
    MatFormFieldModule,
    // IMaskModule,
    AgentInformationComponent,
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
    AddLicenseApptComponent,
    EditEmploymentHistComponent,
    EditTransferHistComponent,
    EditCorequirementHistComponent,
    EditJobtitleHistComponent,
    EditLicApplInfoComponent,
    EditLicPreEduComponent,
    EditLicPreExamComponent,
    EditLicenseRenewalComponent,
    EditHoursTakenComponent,
    EditDiaryEntryComponent,
    TicklerInfoComponent,
    TicklerSearchComponent,
    EditTicklerInfoComponent,
    
    AddTicklerComponent,
    AddIndexerComponent,
    EditTmDetailComponent,
    AddTeamMemberComponent,
    TicklerMgmtComponent,
    SearchTeamMemberComponent,
    SearchListComponent,
    TmInfoManagementComponent,
    InsertIncentiveLicenseComponent,
    InsertMemberLoaTicklerComponent,
    AgentTicklerInfoComponent,
    AgentWishlistItemsComponent,
    RouterModule.forChild(routes)
  ],
  declarations: [
    
    
  ],
  
  exports: [
    AgentInformationComponent,
    AddTeamMemberComponent,
    TicklerMgmtComponent,
    SearchTeamMemberComponent,
    SearchListComponent,
    TmInfoManagementComponent,
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
    EditEmploymentHistComponent,
    EditTransferHistComponent,
    EditCorequirementHistComponent,
    EditJobtitleHistComponent,
    EditLicApplInfoComponent,
    EditLicPreEduComponent,
    EditLicPreExamComponent,
    EditLicenseRenewalComponent,
    EditHoursTakenComponent,
    EditDiaryEntryComponent,
    TicklerInfoComponent,
    TicklerSearchComponent,
    EditTicklerInfoComponent,
    
    AddTicklerComponent,
    AddIndexerComponent,
    InsertIncentiveLicenseComponent,  
  ],
})
export class TeamMemberModule {}
