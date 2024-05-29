import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';

import { EmfTestPageComponent } from './emf-test/emf-test-page.component';
import { ReportsComponent } from './reports/reports/reports.component';
import { WorkListComponent } from './work-list/work-list.component';

const routes: Routes = [
  // Dashboard
  { path: 'dashboard', component: DashboardComponent },
  // Admin
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
  // Team Member
  { path: 'team-member', loadChildren: () => import('./team-member/team-member.module').then(m => m.TeamMemberModule) },
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
