import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';


import { SharedModule } from '../_shared/shared.module';

import { CompanyEditComponent } from './company-edit/company-edit.component';
import { CompanyRequirementsComponent } from './company-requirements/company-requirements.component';
import { ContinueEducationEditComponent } from './continue-education-edit/continue-education-edit.component';
import { DropdownListEditComponent } from './dropdown-list-edit/dropdown-list-edit.component';
import { ExamEditComponent } from './exam-edit/exam-edit.component';
import { JobTitleLicenseComponent } from './job-title-license/job-title-license.component';
import { LicenseEditComponent } from './license-edit/license-edit.component';
import { LicenseTechEditComponent } from './license-tech-edit/license-tech-edit.component';
import { PreEducationEditComponent } from './pre-education-edit/pre-education-edit.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { StateLicenseRequirementsComponent } from './state-license-requirements/state-license-requirements.component';
import { StateProvinceEditComponent } from './state-province-edit/state-province-edit.component';
import { XborLicRequirementsComponent } from './xbor-lic-requirements/xbor-lic-requirements.component';

import { EditCompanyComponent } from './company-edit/edit-company/edit-company.component';
import { EditCoRequirementComponent } from './company-requirements/edit-co-requirement/edit-co-requirement.component';
import { EditEduRuleComponent } from './continue-education-edit/edit-edu-rule/edit-edu-rule.component';
import { EditDropdownItemComponent } from './dropdown-list-edit/edit-dropdown-item/edit-dropdown-item.component';
import { EditExamComponent } from './exam-edit/edit-exam/edit-exam.component';
import { EditCompanyItemComponent } from './license-edit/edit-company-item/edit-company-item.component';
import { EditPreExamItemComponent } from './license-edit/edit-pre-exam-item/edit-pre-exam-item.component';
import { EditPreEduItemComponent } from './license-edit/edit-pre-edu-item/edit-pre-edu-item.component';
import { EditProductItemComponent } from './license-edit/edit-product-item/edit-product-item.component';
import { EditJobTitleComponent } from './job-title-license/edit-job-title/edit-job-title.component';
import { EditLicenseTechComponent } from './license-tech-edit/edit-license-tech/edit-license-tech.component';
import { EditPreEducationComponent } from './pre-education-edit/edit-pre-education/edit-pre-education.component';
import { EditStateRequirementComponent } from './state-license-requirements/edit-state-requirement/edit-state-requirement.component';
import { EditStateProvinceComponent } from './state-province-edit/edit-state-province/edit-state-province.component';
import { EditXborRequirementComponent } from './xbor-lic-requirements/edit-xbor-requirement/edit-xbor-requirement.component';

import { EditProductComponent } from './product-edit/edit-product/edit-product.component';
import { AddCompanyComponent } from './license-edit/add-company/add-company.component';
import { AddPreExamComponent } from './license-edit/add-pre-exam/add-pre-exam.component';
import { AddPreEduComponent } from './license-edit/add-pre-edu/add-pre-edu.component';
import { AddProductComponent } from './license-edit/add-product/add-product.component';

@NgModule({
  declarations: [
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
    AddCompanyComponent,
    AddPreExamComponent,
    AddPreEduComponent,
    AddProductComponent,
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
    EditPreExamItemComponent,
    EditPreEduItemComponent,
    EditProductItemComponent,
    EditJobTitleComponent,
    LicenseEditComponent,
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
    AddCompanyComponent,
    AddPreExamComponent,
    AddPreEduComponent,
    AddProductComponent,
  ],
})
export class AdminModule {}
