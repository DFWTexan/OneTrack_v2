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
  { path: 'team/search-members', component: SearchTeamMemberComponent },
  // Reports
  { path: 'reports', component: ReportsComponent },
  // Work List
  { path: 'work-lists', component: WorkListComponent },

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
