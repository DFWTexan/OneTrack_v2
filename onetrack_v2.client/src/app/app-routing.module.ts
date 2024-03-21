import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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
import { EmfTestPageComponent } from './emf-test/emf-test-page.component';
import { TmInformationComponent } from './team-member/agent-information/tm-information/tm-information.component';
import { TmDetailComponent } from './team-member/agent-information/tm-detail/tm-detail.component';
import { TmEmptransHistoryComponent } from './team-member/agent-information/tm-emptrans-history/tm-emptrans-history.component';
import { TmContinuingEduComponent } from './team-member/agent-information/tm-continuing-edu/tm-continuing-edu.component';
import { TmDiaryComponent } from './team-member/agent-information/tm-diary/tm-diary.component';
import { TmEmailComponent } from './team-member/agent-information/tm-email/tm-email.component';
import { TmCommunicationsComponent } from './team-member/agent-information/tm-communications/tm-communications.component';
import { TicklerInfoComponent } from './_components/tickler-mgmt/tickler-info/tickler-info.component';
import { TicklerSearchComponent } from './_components/tickler-mgmt/tickler-search/tickler-search.component';

const routes: Routes = [
  // Dashboard
  { path: 'dashboard', component: DashboardComponent },
  // Admin
  { path: 'admin/company-edit', component: CompanyEditComponent },
  { path: 'admin/company-requirements', component: CompanyRequirementsComponent },
  { path: 'admin/con-education-edit', component: ContinueEducationEditComponent },
  { path: 'admin/dropdown-list-edit', component: DropdownListEditComponent },
  { path: 'admin/examp-edit', component: ExamEditComponent },
  { path: 'admin/job-title-license', component: JobTitleLicenseComponent },
  { path: 'admin/license-edit', component: LicenseEditComponent },
  { path: 'admin/license-tech-edit', component: LicenseTechEditComponent },
  { path: 'admin/pre-education-edit', component: PreEducationEditComponent },
  { path: 'admin/product-edit', component: ProductEditComponent },
  { path: 'admin/state-lic-reuirements', component: StateLicenseRequirementsComponent },
  { path: 'admin/state-province-edit', component: StateProvinceEditComponent },
  { path: 'admin/xbor-lic-requirements', component: XborLicRequirementsComponent },
  // Team Member
  { path: 'team/add-member', component: AddTeamMemberComponent },
  // { path: 'team/search-members', component: SearchTeamMemberComponent },
  { 
    path: 'team/search-members', 
    component: SearchTeamMemberComponent,
    children: [
      { path: 'tic-info', component: TicklerInfoComponent },
      { path: 'tic-search', component: TicklerSearchComponent },
    ]
  },
  { path: 'team/agent-info/:id', 
    component: AgentInformationComponent,
    children: [
      { path: '', redirectTo: 'tm-info', pathMatch: 'full' },
      { path: 'tm-info', component: TmInformationComponent },
      { path: 'tm-detail', component: TmDetailComponent },
      { path: 'tm-emp-tran-his', component: TmEmptransHistoryComponent },
      { path: 'tm-con-ed', component: TmContinuingEduComponent },
      { path: 'tm-diary', component: TmDiaryComponent },
      { path: 'tm-email', component: TmEmailComponent },
      { path: 'tm-communications', component: TmCommunicationsComponent },
      // other child routes...
    ]
  },
  // Reports
  // Reports
  { path: 'reports', component: ReportsComponent },
  // Work List
  { path: 'work-lists', component: WorkListComponent },
  // Test page 
  { path: 'emf-test', component: EmfTestPageComponent },

  // Redirect to dashboard or any other component as the default route
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  // Add a wildcard route for 404 not found pages
  // { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
