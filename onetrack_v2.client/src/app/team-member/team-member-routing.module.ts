import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddTeamMemberComponent } from '../team-member/add-team-member/add-team-member.component';
import { AgentInformationComponent } from '../team-member/agent-information/agent-information.component';
import { SearchTeamMemberComponent } from '../team-member/search-team-member/search-team-member.component';
import { TmInfoManagementComponent } from '../team-member/agent-information/tm-info-management/tm-info-management.component';
import { TmLicenseManagementComponent } from '../team-member/agent-information/tm-license-management/tm-license-management.component';
// import { TicklerInfoComponent } from '../_components/tickler-mgmt/tickler-info/tickler-info.component';
// import { TicklerSearchComponent } from '../_components/tickler-mgmt/tickler-search/tickler-search.component';

const routes: Routes = [
  { path: 'team/add-member', component: AddTeamMemberComponent },
  {
    path: 'team/search-members', component: SearchTeamMemberComponent //,
    // children: [
    //   { path: 'tic-info', component: TicklerInfoComponent },
    //   { path: 'tic-search', component: TicklerSearchComponent },
    // ],
  },
  {
    path: 'team/agent-info/:id',
    component: AgentInformationComponent,
    children: [
      { path: '', redirectTo: 'tm-info', pathMatch: 'full' },
      { path: 'tm-info-mgmt', component: TmInfoManagementComponent },
      { path: 'tm-license-mgmt', component: TmLicenseManagementComponent },
      // other child routes...
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeamMemberRoutingModule {}
