import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ConfigService, ConstantsDataService } from '../../_services';

@NgModule({
  imports: [
    CommonModule
  ],
  providers: [
    ConstantsDataService,
    ConfigService,
    // Add other app-wide singleton services here
  ],
  declarations: [
    // Components used once in the application (like header/footer)
  ],
  exports: [
    // Components that should be available app-wide
  ]
})
export class CoreModule {
  // Guard against reimporting CoreModule
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule is already loaded. Import it in the AppModule only.');
    }
  }
}