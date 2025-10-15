import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { WorkListComponent } from '../../work-list/work-list.component';

const routes: Routes = [
  { path: '', component: WorkListComponent }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    WorkListComponent
  ]
})
export class WorkModule {}