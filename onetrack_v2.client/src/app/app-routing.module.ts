import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { CompanyEditComponent } from './admin/company-edit/company-edit.component';
import { CompanyRequirementsComponent } from './admin/company-requirements/company-requirements.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'admin/company-edit', component: CompanyEditComponent },
  { path: 'admin/company-requirements', component: CompanyRequirementsComponent },
  // { path: 'administration/con-education-edit', component: ConEducationEditComponent },
  // { path: 'team/add-member', component: AddTeamMemberComponent },
  // { path: 'team/search-members', component: SearchMembersComponent },
  // { path: 'reports', component: ReportsComponent },
  // { path: 'work-lists', component: WorkListsComponent },
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
