import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { TechWorklistComponent } from '../../_appModals/tech-worklist/tech-worklist.component';
import { TechTicklerItemsComponent } from '../../_appModals/tech-tickler-items/tech-tickler-items.component';
// Import other modal components

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    TechWorklistComponent,
    TechTicklerItemsComponent,
  ],
  declarations: [
    
    // Other modal components
  ],
  exports: [
    TechWorklistComponent,
    TechTicklerItemsComponent,
    // Other modal components that need to be accessible
  ]
})
export class ModalsModule {}