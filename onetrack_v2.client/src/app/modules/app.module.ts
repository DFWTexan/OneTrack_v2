// Angular core
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

// App modules
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { ModalsModule } from './appModals/modals.module';

// App components
import { AppComponent } from '../app.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { EmfTestPageComponent } from '../emf-test/emf-test-page.component';
import { LoginComponent } from '../_components/login/login.component';
import { QuickFindComponent } from '../_components/quick-find/quick-find.component';
import { ImpersonationComponent } from '../_components/impersonation/impersonation.component';
import { ReportsComponent } from '../reports/reports/reports.component';

// Services
import { ConfigService } from '../_services/';

export function initializeApp(configService: ConfigService) {
  return () => configService.loadConfig();
}

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    // ModalsModule,
    ReportsComponent, // As a standalone component
    AppComponent,
    DashboardComponent,
    EmfTestPageComponent,
    LoginComponent,
    QuickFindComponent,
    ImpersonationComponent,
  ],
  declarations: [
    
    // IncentiveInfoComponent should be moved to TeamMemberModule
  ],
  providers: [
    provideAnimationsAsync(),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true,
    },
  ],
})
export class AppModule {}

// Bootstrap the standalone AppComponent
bootstrapApplication(AppComponent, {
  providers: [
    provideAnimationsAsync(),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true,
    },
  ]
});