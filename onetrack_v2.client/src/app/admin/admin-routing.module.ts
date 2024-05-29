import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CompanyEditComponent } from '../admin/company-edit/company-edit.component';
import { CompanyRequirementsComponent } from '../admin/company-requirements/company-requirements.component';
import { ContinueEducationEditComponent } from '../admin/continue-education-edit/continue-education-edit.component';
import { DropdownListEditComponent } from '../admin/dropdown-list-edit/dropdown-list-edit.component';
import { ExamEditComponent } from '../admin/exam-edit/exam-edit.component';
import { JobTitleLicenseComponent } from '../admin/job-title-license/job-title-license.component';
import { LicenseInfoComponent } from '../team-member/agent-information/tm-license-management/license-info/license-info.component';
import { LicenseEditComponent } from '../admin/license-edit/license-edit.component';
import { LicenseTechEditComponent } from '../admin/license-tech-edit/license-tech-edit.component';
import { PreEducationEditComponent } from '../admin/pre-education-edit/pre-education-edit.component';
import { ProductEditComponent } from '../admin/product-edit/product-edit.component';
import { StateLicenseRequirementsComponent } from '../admin/state-license-requirements/state-license-requirements.component';
import { StateProvinceEditComponent } from '../admin/state-province-edit/state-province-edit.component';
import { XborLicRequirementsComponent } from '../admin/xbor-lic-requirements/xbor-lic-requirements.component';

const routes: Routes = [
  { path: 'admin/company-edit', component: CompanyEditComponent },
  {
    path: 'admin/company-requirements',
    component: CompanyRequirementsComponent,
  },
  {
    path: 'admin/con-education-edit',
    component: ContinueEducationEditComponent,
  },
  { path: 'admin/dropdown-list-edit', component: DropdownListEditComponent },
  { path: 'admin/examp-edit', component: ExamEditComponent },
  { path: 'admin/job-title-license', component: JobTitleLicenseComponent },
  { path: 'admin/license-edit', component: LicenseEditComponent },
  { path: 'admin/license-tech-edit', component: LicenseTechEditComponent },
  { path: 'admin/pre-education-edit', component: PreEducationEditComponent },
  { path: 'admin/product-edit', component: ProductEditComponent },
  {
    path: 'admin/state-lic-reuirements',
    component: StateLicenseRequirementsComponent,
  },
  { path: 'admin/state-province-edit', component: StateProvinceEditComponent },
  {
    path: 'admin/xbor-lic-requirements',
    component: XborLicRequirementsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
