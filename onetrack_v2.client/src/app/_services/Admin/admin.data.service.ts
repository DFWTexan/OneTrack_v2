import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AdminComService } from './admin.com.service';
import { Company, CompanyRequirement, EducationRule } from '../../_Models';
import { ModalService } from '../common/modal.service';

@Injectable({
  providedIn: 'root',
})
export class AdminDataService {
  private apiUrl: string = environment.apiUrl + 'Admin/';

  // COMPANY
  companyTypes: any[] = [];
  companyTypesChanged = new Subject<any[]>();
  companies: Company[] = [];
  companiesChanged = new Subject<Company[]>();
  company: Company = {} as Company;
  companyChanged = new Subject<Company>();
  // COMPANY REQUIREMENTS
  companyRequirements: CompanyRequirement[] = [];
  companyRequirementsChanged = new Subject<CompanyRequirement[]>();
  coRequirement: CompanyRequirement = {} as CompanyRequirement;
  coRequirementChanged = new Subject<CompanyRequirement>();
  // CONTINUE EDUCATION
  licenseTypes: any[] = [];
  licenseTypesChanged = new Subject<any[]>();
  educationRule: EducationRule = {} as EducationRule;
  educationRuleChanged = new Subject<EducationRule>();
  // DROPDOWN LIST
  dropdownListTypes: any[] = [];
  dropdownListTypesChanged = new Subject<any[]>();
  dropdownListItems: any[] = [];
  dropdownListItemsChanged = new Subject<any[]>();
  dropdownListItem: {} = {};
  dropdownListItemChanged = new Subject<any>();

  constructor(
    private http: HttpClient,
    private adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  // COMPANY
  fetchCompanyTypes() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: [];
        errMessage: string;
      }>(this.apiUrl+ 'GetCompanyTypes')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.companyTypes = response.objData;
            this.companyTypesChanged.next(this.companyTypes);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchCompanies(companyType: string) {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Company[];
        errMessage: string;
      }>(this.apiUrl + 'GetCompaniesByType/' + companyType)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.companies = response.objData;
            this.companiesChanged.next(this.companies);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // COMPANY REQUIREMENTS
  fetchCompanyRequirements(workState: string, resState: string | null = null) {
    const queryParams = `?workState=${workState}&resState=${
      resState ? resState : ''
    }`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: CompanyRequirement[];
        errMessage: string;
      }>(`${this.apiUrl}GetCompanyRequirements${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.companyRequirements = response.objData;
            this.companyRequirementsChanged.next(this.companyRequirements);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // CONTINUE EDUCATION
  fetchLicenseTypes() {
   return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetLicenseTypes')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseTypes = response.objData;
            this.licenseTypesChanged.next(this.licenseTypes);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchEducationRules(stateAbv: string | null, licenseType: string | null) {
    const queryParams = `?stateAbv=${stateAbv}&licenseType=${licenseType}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetConEducationRules/${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // DROPDOWN LIST
  fetchDropdownListTypes() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetDropdownListTypes')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchDropdownListItems(dropdownListType: string) {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetDropdownByType/' + dropdownListType)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.dropdownListItems = response.objData;
            this.dropdownListItemsChanged.next(this.dropdownListItems);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // STORE
  storeCompany(mode: string | '', company: any | null) {
    this.adminComService.modeCompanyModal(mode);
    this.company = company || {};
    this.companyChanged.next(this.company);
  }

  storeCoRequirement(mode: string | '', coRequirement: any | null) {
    this.adminComService.modeCoRequirementModal(mode);
    this.coRequirement = coRequirement || {};
    this.coRequirementChanged.next(this.coRequirement);
  }

  storeEducationRule(mode: string | '', educationRule: any | null) {
    this.adminComService.modeEducationRuleModal(mode);
    this.educationRule = educationRule || {};
    this.educationRuleChanged.next(this.educationRule);
  }

  storeDopdownItem(mode: string | '', dropdownItem: any | null) {
    this.adminComService.modeDropdownItemModal(mode);
    this.dropdownListItem = dropdownItem || {};
    this.dropdownListItemChanged.next(this.dropdownListItem);
  }
}
