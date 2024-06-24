// Angular core
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

// Angular Material
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';

// App modules
import { SharedModule } from './_shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { TeamMemberModule } from './team-member/team-member.module';
import { AdminModule } from './admin/admin.module';

// App components
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReportsComponent } from './reports/reports/reports.component';
import { WorkListComponent } from './work-list/work-list.component';
import { EmfTestPageComponent } from './emf-test/emf-test-page.component';
import { LoginComponent } from './_components/login/login.component';

// App services
import { ConstantsDataService } from './_services/';

// Other
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
// import { FileUploadComponent } from './_components/file-upload/file-upload.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ReportsComponent,
    WorkListComponent,
    EmfTestPageComponent,
    LoginComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    RouterModule,
    SharedModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatDividerModule,
    TeamMemberModule,
    AdminModule,
    RouterModule.forRoot([])
  ],
  providers: [provideAnimationsAsync(), ConstantsDataService],
  bootstrap: [AppComponent],
})
export class AppModule {}