import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from '../dashboard/dashboard.component';
import { LoginComponent } from '../_components/login/login.component';
import { EmfTestPageComponent } from '../emf-test/emf-test-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'login', component: LoginComponent },
  { path: 'emf-test', component: EmfTestPageComponent },
  { 
    path: 'work-lists', 
    loadChildren: () => import('./work/work.module').then(m => m.WorkModule)
  },
  { 
    path: 'team', 
    loadChildren: () => import('./team-member/team-member.module').then(m => m.TeamMemberModule)
  },
  { 
    path: 'admin', 
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  },
  { path: '**', redirectTo: 'dashboard' } // Wildcard route for 404
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}