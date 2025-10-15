import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

// Component imports
import { CompanyEditComponent } from '../../admin/company-edit/company-edit.component';
import { CompanyRequirementsComponent } from '../../admin/company-requirements/company-requirements.component';
import { ContinueEducationEditComponent } from '../../admin/continue-education-edit/continue-education-edit.component';
import { DropdownListEditComponent } from '../../admin/dropdown-list-edit/dropdown-list-edit.component';
import { ExamEditComponent } from '../../admin/exam-edit/exam-edit.component';
import { JobTitleLicenseComponent } from '../../admin/job-title-license/job-title-license.component';
import { LicenseEditComponent } from '../../admin/license-edit/license-edit.component';
import { LicenseTechEditComponent } from '../../admin/license-tech-edit/license-tech-edit.component';
import { PreEducationEditComponent } from '../../admin/pre-education-edit/pre-education-edit.component';
import { ProductEditComponent } from '../../admin/product-edit/product-edit.component';
import { StateLicenseRequirementsComponent } from '../../admin/state-license-requirements/state-license-requirements.component';
import { StateProvinceEditComponent } from '../../admin/state-province-edit/state-province-edit.component';
import { XborLicRequirementsComponent } from '../../admin/xbor-lic-requirements/xbor-lic-requirements.component';

// Edit component imports
import { EditCompanyComponent } from '../../admin/company-edit/edit-company/edit-company.component';
import { EditCoRequirementComponent } from '../../admin/company-requirements/edit-co-requirement/edit-co-requirement.component';
import { EditEduRuleComponent } from '../../admin/continue-education-edit/edit-edu-rule/edit-edu-rule.component';
import { EditDropdownItemComponent } from '../../admin/dropdown-list-edit/edit-dropdown-item/edit-dropdown-item.component';
import { EditExamComponent } from '../../admin/exam-edit/edit-exam/edit-exam.component';
import { EditCompanyItemComponent } from '../../admin/license-edit/edit-company-item/edit-company-item.component';
import { EditPreExamItemComponent } from '../../admin/license-edit/edit-pre-exam-item/edit-pre-exam-item.component';
import { EditPreEduItemComponent } from '../../admin/license-edit/edit-pre-edu-item/edit-pre-edu-item.component';
import { EditProductItemComponent } from '../../admin/license-edit/edit-product-item/edit-product-item.component';
import { EditJobTitleComponent } from '../../admin/job-title-license/edit-job-title/edit-job-title.component';
import { EditLicenseTechComponent } from '../../admin/license-tech-edit/edit-license-tech/edit-license-tech.component';
import { EditPreEducationComponent } from '../../admin/pre-education-edit/edit-pre-education/edit-pre-education.component';
import { EditStateRequirementComponent } from '../../admin/state-license-requirements/edit-state-requirement/edit-state-requirement.component';
import { EditStateProvinceComponent } from '../../admin/state-province-edit/edit-state-province/edit-state-province.component';
import { EditXborRequirementComponent } from '../../admin/xbor-lic-requirements/edit-xbor-requirement/edit-xbor-requirement.component';
import { EditProductComponent } from '../../admin/product-edit/edit-product/edit-product.component';

// Add component imports
import { AddCompanyComponent } from '../../admin/license-edit/add-company/add-company.component';
import { AddPreExamComponent } from '../../admin/license-edit/add-pre-exam/add-pre-exam.component';
import { AddPreEduComponent } from '../../admin/license-edit/add-pre-edu/add-pre-edu.component';
import { AddProductComponent } from '../../admin/license-edit/add-product/add-product.component';

const routes: Routes = [
  { path: 'license-edit', component: LicenseEditComponent },
  { path: 'company-edit', component: CompanyEditComponent },
  { path: 'requirements-edit', component: CompanyRequirementsComponent },
  { path: 'continue-education-edit', component: ContinueEducationEditComponent },
  { path: 'dropdown-list-edit', component: DropdownListEditComponent },
  { path: 'exam-edit', component: ExamEditComponent },
  { path: 'job-title-license', component: JobTitleLicenseComponent },
  { path: 'license-tech-edit', component: LicenseTechEditComponent },
  { path: 'pre-education-edit', component: PreEducationEditComponent },
  { path: 'product-edit', component: ProductEditComponent },
  { path: 'state-license-requirements', component: StateLicenseRequirementsComponent },
  { path: 'state-province-edit', component: StateProvinceEditComponent },
  { path: 'xbor-lic-requirements', component: XborLicRequirementsComponent },
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild(routes),
    
    // Standalone components moved here from declarations
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
    
    // Standalone edit components
    EditCompanyComponent,
    EditCoRequirementComponent,
    EditEduRuleComponent,
    EditDropdownItemComponent,
    EditExamComponent,
    EditCompanyItemComponent,
    EditPreExamItemComponent,
    EditPreEduItemComponent,
    EditProductItemComponent,
    EditJobTitleComponent,
    EditLicenseTechComponent,
    EditPreEducationComponent,
    EditStateRequirementComponent,
    EditStateProvinceComponent,
    EditXborRequirementComponent,
    EditProductComponent,
    
    // Standalone add components
    AddCompanyComponent,
    AddPreExamComponent,
    AddPreEduComponent,
    AddProductComponent
  ],
  declarations: [
    // Leave this array empty if all components are standalone
    // Or move non-standalone components back here if any
  ],
  exports: [
    // For standalone components, they need to be re-exported if needed elsewhere
    LicenseEditComponent
  ]
})
export class AdminModule {}