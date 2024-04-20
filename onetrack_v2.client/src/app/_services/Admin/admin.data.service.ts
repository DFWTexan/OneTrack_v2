import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { AdminComService } from './admin.com.service';
import {
  Company,
  CompanyItem,
  CompanyRequirement,
  EducationRule,
  Exam,
  JobTitle,
  License,
  LicenseTech,
  PreEduItem,
  PreEducation,
  PreExamItem,
  Product,
  ProductItem,
  StateProvince,
  StateRequirement,
  XborLicenseRequirement,
} from '../../_Models';
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
  // EXAM EDIT
  examItems: Exam[] = [];
  examItemsChanged = new Subject<Exam[]>();
  // JOB TITLE
  licenseLevels: any[] = [];
  licenseLevelsChanged = new Subject<any[]>();
  licenseIncentives: any[] = [];
  licenseIncentivesChanged = new Subject<any[]>();
  jobTitles: JobTitle[] = [];
  jobTitlesChanged = new Subject<JobTitle[]>();
  // LICENSE EDIT
  licenseItems: License[] = [];
  licenseItemsChanged = new Subject<License[]>();
  licenseItem: License = {} as License;
  licenseItemChanged = new Subject<License>();
  companyItem: CompanyItem = {} as CompanyItem;
  companyItemChanged = new Subject<CompanyItem>();
  preExamItem: PreExamItem = {} as PreExamItem;
  preExamItemChanged = new Subject<PreExamItem>();
  preEduItem: PreEduItem = {} as PreEduItem;
  preEduItemChanged = new Subject<PreEduItem>();
  productItem: ProductItem = {} as ProductItem;
  productItemChanged = new Subject<ProductItem>();
  licenseTech: LicenseTech = {} as LicenseTech;
  licenseTechChanged = new Subject<LicenseTech>();
  preEducation: PreEducation = {} as PreEducation;
  preEducationChanged = new Subject<PreEducation>();
  product: Product = {} as Product;
  productChanged = new Subject<Product>();
  stateRequirements: StateRequirement[] = [];
  stateRequirementsChanged = new Subject<StateRequirement[]>();
  stateRequirement: StateRequirement = {} as StateRequirement;
  stateRequirementChanged = new Subject<StateRequirement>();
  stateProvinces: StateProvince[] = [];
  stateProvincesChanged = new Subject<StateProvince[]>();
  stateProvince: StateProvince = {} as StateProvince;
  stateProvinceChanged = new Subject<StateProvince>();
  xborLicenseRequirement: StateRequirement = {} as XborLicenseRequirement;
  xborLicenseRequirementChanged = new Subject<XborLicenseRequirement>();

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
      }>(this.apiUrl + 'GetCompanyTypes')
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

  // EXAM EDIT
  fetchExamItems(stateProvince: string) {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetExamByState/' + stateProvince)
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

  // JOB TITLE
  fetchLicenseLevels() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetJobTitleLicLevel')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseLevels = response.objData;
            this.licenseLevelsChanged.next(this.licenseLevels);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchLicenseIncentives() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetJobTitlelicIncentive')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseIncentives = response.objData;
            this.licenseIncentivesChanged.next(this.licenseIncentives);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchJobTitles() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: JobTitle[];
        errMessage: string;
      }>(this.apiUrl + 'GetJobTitles')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.jobTitles = response.objData;
            this.jobTitlesChanged.next(this.jobTitles);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  filterJobTitleData(filterJobTitle: string) {
    let filteredJobTitles: any[] = [];
    // console.log(
    //   'EMFTEST (admin.Service) - filterJobTitleData: ',
    //   filterJobTitle
    // );

    if (
      filterJobTitle !== '' &&
      filterJobTitle !== null &&
      filterJobTitle !== undefined
    ) {
      filteredJobTitles = this.jobTitles.filter((jobTitle) =>
        jobTitle.jobTitle1.toLowerCase().includes(filterJobTitle.toLowerCase())
      );
    }

    // console.log('EMFTEST (admin.Service) - Filtered job titles => \n ', filteredJobTitles);

    this.jobTitlesChanged.next(filteredJobTitles);
  }

  //  LICENSE EDIT
  fetchLicenseItems(stateProvince: string) {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: License[];
        errMessage: string;
      }>(this.apiUrl + 'GetLicenseByStateProv/' + stateProvince)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseItems = response.objData;
            this.licenseItemsChanged.next(this.licenseItems);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // LICENSE TECH
  fetchLicenseTechs() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: LicenseTech[];
        errMessage: string;
      }>(this.apiUrl + 'GetLicTechList')
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

  // PRE-EDUCATION
  fetchPreEducationItems(stateProvince: string) {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: PreEducation[];
        errMessage: string;
      }>(this.apiUrl + 'GetPreEduEditByState/' + stateProvince)
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

  // PRODUCT
  fetchProducts() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Product[];
        errMessage: string;
      }>(this.apiUrl + 'GetProductEdits')
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

  // STATE REQUIREMENT
  fetchStateRequirements(
    workState: string | null = null,
    resState: string | null = null,
    branchCode: string | null = null
  ) {
    const queryParams = `?workState=${workState ? workState : ''}&resState=${
      resState ? resState : ''
    }&branchCode=${branchCode ? branchCode : ''}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: StateRequirement[];
        errMessage: string;
      }>(`${this.apiUrl}GetStateLicRequirements${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.stateRequirements = response.objData;
            this.stateRequirementChanged.next(this.stateRequirement);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // STATE PROVINCE
  fetchStateProvinces() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: StateProvince[];
        errMessage: string;
      }>(this.apiUrl + 'GetStateProvinceList')
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

  // XBOR REQUIREMENTS
  fetchXBorBranchCodes() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetXBorderBranchCodes')
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
  fetchXBorLicRequirements(branchCode: string) {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetXBorLicRequirements/' + branchCode)
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

  ///////////////////////////////////////////// STORE DATA //////////////////////////////////////////
  storeCompany(mode: string | '', company: any | null) {
    this.adminComService.changeMode('company', mode);
    this.company = company || {};
    this.companyChanged.next(this.company);
  }

  storeCoRequirement(mode: string | '', coRequirement: any | null) {
    this.adminComService.changeMode('coRequirement', mode);
    this.coRequirement = coRequirement || {};
    this.coRequirementChanged.next(this.coRequirement);
  }

  storeEducationRule(mode: string | '', educationRule: any | null) {
    this.adminComService.changeMode('educationRule', mode);
    this.educationRule = educationRule || {};
    this.educationRuleChanged.next(this.educationRule);
  }

  storeDopdownItem(mode: string | '', dropdownItem: any | null) {
    this.adminComService.changeMode('dropdownItem', mode);
    this.dropdownListItem = dropdownItem || {};
    this.dropdownListItemChanged.next(this.dropdownListItem);
  }

  storeExamItem(mode: string | '', examItem: any | null) {
    this.adminComService.changeMode('examItem', mode);
    this.examItems = examItem || [];
    this.examItemsChanged.next(this.examItems);
  }

  storeJobTitle(mode: string | '', jobTitle: any | null) {
    this.adminComService.changeMode('jobTitle', mode);
    this.dropdownListItem = jobTitle || {};
    this.dropdownListItemChanged.next(this.dropdownListItem);
  }

  storeLicenseItem(mode: string | '', licenseItem: any | null) {
    this.adminComService.changeMode('licenseItem', mode);
    this.licenseItem = licenseItem || {};
    this.licenseItemChanged.next(this.licenseItem);
  }

  storeCompanyItem(mode: string | '', companyItem: any | null) {
    this.adminComService.changeMode('companyItem', mode);
    this.companyItem = companyItem || {};
    this.companyItemChanged.next(this.companyItem);
  }

  storePreExamItem(mode: string | '', preExamItem: any | null) {
    this.adminComService.changeMode('preExamItem', mode);
    this.preExamItem = preExamItem || {};
    this.preExamItemChanged.next(this.preExamItem);
  }

  storePreEduItem(mode: string | '', preEduItem: any | null) {
    this.adminComService.changeMode('preEduItem', mode);
    this.preEduItem = preEduItem || {};
    this.preEduItemChanged.next(this.preEduItem);
  }

  storeProductItem(mode: string | '', productItem: any | null) {
    this.adminComService.changeMode('productItem', mode);
    this.productItem = productItem || {};
    this.productItemChanged.next(this.productItem);
  }

  storeLicenseTech(mode: string | '', licenseTech: any | null) {
    this.adminComService.changeMode('licenseTech', mode);
    this.licenseTech = licenseTech || {};
    this.licenseTechChanged.next(this.licenseTech);
  }

  storePreEducation(mode: string | '', preEducation: any | null) {
    this.adminComService.changeMode('preEducation', mode);
    this.preEducation = preEducation || {};
    this.preEducationChanged.next(this.preEducation);
  }

  storeProduct(mode: string | '', product: any | null) {
    this.adminComService.changeMode('product', mode);
    this.product = product || {};
    this.productChanged.next(this.product);
  }

  storeStateRequirement(mode: string | '', stateRequirement: any | null) {
    this.adminComService.changeMode('stateRequirement', mode);
    this.stateRequirement = stateRequirement || {};
    this.stateRequirementChanged.next(this.stateRequirement);
  }

  storeStateProvince(mode: string | '', stateProvince: any | null) {
    this.adminComService.changeMode('stateProvince', mode);
    this.stateProvince = stateProvince || {};
    this.stateProvinceChanged.next(this.stateProvince);
  }

  storeXBorRequirement(mode: string | '', xBorRequirement: any | null) {
    this.adminComService.changeMode('xborLicenseRequirement', mode);
    this.xborLicenseRequirement = xBorRequirement || {};
    this.xborLicenseRequirementChanged.next(this.xborLicenseRequirement);
  }
}
