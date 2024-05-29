import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';


import { SharedModule } from '../_shared/shared.module';

import { CompanyEditComponent } from '../admin/company-edit/company-edit.component';
import { CompanyRequirementsComponent } from '../admin/company-requirements/company-requirements.component';
import { ContinueEducationEditComponent } from '../admin/continue-education-edit/continue-education-edit.component';
import { DropdownListEditComponent } from '../admin/dropdown-list-edit/dropdown-list-edit.component';
import { ExamEditComponent } from '../admin/exam-edit/exam-edit.component';
import { JobTitleLicenseComponent } from '../admin/job-title-license/job-title-license.component';

import { LicenseTechEditComponent } from '../admin/license-tech-edit/license-tech-edit.component';
import { PreEducationEditComponent } from '../admin/pre-education-edit/pre-education-edit.component';
import { ProductEditComponent } from '../admin/product-edit/product-edit.component';
import { StateLicenseRequirementsComponent } from '../admin/state-license-requirements/state-license-requirements.component';
import { StateProvinceEditComponent } from '../admin/state-province-edit/state-province-edit.component';
import { XborLicRequirementsComponent } from '../admin/xbor-lic-requirements/xbor-lic-requirements.component';

import { EditCompanyComponent } from '../admin/company-edit/edit-company/edit-company.component';
import { EditCoRequirementComponent } from '../admin/company-requirements/edit-co-requirement/edit-co-requirement.component';
import { EditEduRuleComponent } from '../admin/continue-education-edit/edit-edu-rule/edit-edu-rule.component';
import { EditDropdownItemComponent } from '../admin/dropdown-list-edit/edit-dropdown-item/edit-dropdown-item.component';
import { EditExamComponent } from '../admin/exam-edit/edit-exam/edit-exam.component';
import { EditCompanyItemComponent } from '../admin/license-edit/edit-company-item/edit-company-item.component';
import { EditJobTitleComponent } from '../admin/job-title-license/edit-job-title/edit-job-title.component';
import { EditLicenseTechComponent } from '../admin/license-tech-edit/edit-license-tech/edit-license-tech.component';
import { EditPreEducationComponent } from '../admin/pre-education-edit/edit-pre-education/edit-pre-education.component';
import { EditStateRequirementComponent } from '../admin/state-license-requirements/edit-state-requirement/edit-state-requirement.component';
import { EditStateProvinceComponent } from '../admin/state-province-edit/edit-state-province/edit-state-province.component';
import { EditXborRequirementComponent } from '../admin/xbor-lic-requirements/edit-xbor-requirement/edit-xbor-requirement.component';

import { EditProductComponent } from '../admin/product-edit/edit-product/edit-product.component';

@NgModule({
  declarations: [
    CompanyEditComponent,
    CompanyRequirementsComponent,
    ContinueEducationEditComponent,
    DropdownListEditComponent,
    ExamEditComponent,
    JobTitleLicenseComponent,
    LicenseTechEditComponent,
    PreEducationEditComponent,
    ProductEditComponent,
    StateLicenseRequirementsComponent,
    StateProvinceEditComponent,
    XborLicRequirementsComponent,
    EditCompanyComponent,
    EditCoRequirementComponent,
    EditEduRuleComponent,
    EditDropdownItemComponent,
    EditExamComponent,
    EditCompanyItemComponent,
    EditJobTitleComponent,
    EditLicenseTechComponent,
    EditPreEducationComponent,
    EditStateRequirementComponent,
    EditStateProvinceComponent,
    EditXborRequirementComponent,
    EditProductComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    AdminRoutingModule,
  ],
  exports: [
    EditCompanyComponent,
    EditCoRequirementComponent,
    EditEduRuleComponent,
    EditDropdownItemComponent,
    EditExamComponent,
    EditCompanyItemComponent,
    EditJobTitleComponent,
    EditLicenseTechComponent,
    EditPreEducationComponent,
    EditStateRequirementComponent,
    EditStateProvinceComponent,
    EditXborRequirementComponent,
    EditProductComponent,

    CompanyEditComponent,
    CompanyRequirementsComponent,
    ContinueEducationEditComponent,
    DropdownListEditComponent,
    ExamEditComponent,
    JobTitleLicenseComponent,
    LicenseTechEditComponent,
    PreEducationEditComponent,
    ProductEditComponent,
    StateLicenseRequirementsComponent,
    StateProvinceEditComponent,
    XborLicRequirementsComponent,
  ],
})
export class AdminModule {}
