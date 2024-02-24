import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule, Routes }   from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ModalComponent } from './_components';
import { NultiSelectDropdownComponent } from './_components/multi-select/nulti-select-dropdown/nulti-select-dropdown.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SideMenuNavComponent } from './side-menu-nav/side-menu-nav/side-menu-nav.component';
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
import { AgentInformationComponent } from './team-member/agent-information/agent-information.component';


@NgModule({
  declarations: [
    AppComponent,
    ModalComponent,
    SideMenuNavComponent,
    NultiSelectDropdownComponent,
    DashboardComponent,
    CompanyEditComponent,
    CompanyRequirementsComponent,
    ContinueEducationEditComponent,
    DropdownListEditComponent,
    ExamEditComponent,
    JobTitleLicenseComponent,
    LicenseEditComponent,
    LicenseTechEditComponent,
    PreEducationEditComponent,
    ProductEditComponent,
    StateLicenseRequirementsComponent,
    StateProvinceEditComponent,
    XborLicRequirementsComponent,
    AddTeamMemberComponent,
    SearchTeamMemberComponent,
    ReportsComponent,
    WorkListComponent,
    AgentInformationComponent
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    MatButtonModule,
    MatSidenavModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    RouterModule,
    MatExpansionModule,
    MatTooltipModule,
    RouterModule.forRoot([])
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
