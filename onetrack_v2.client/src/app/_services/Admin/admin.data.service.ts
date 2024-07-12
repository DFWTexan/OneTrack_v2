import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map, tap } from 'rxjs/operators';

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
  companyType: string = '';
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
  jobTitlesFilter: JobTitle[] = [];
  jobTitlesFilterChanged = new Subject<JobTitle[]>();
  // LICENSE EDIT
  licenseID: number = 0;
  licenseIdChanged = new Subject<number>();
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

  updateCompanyType(companyType: string) {
    this.companyType = companyType;
  }

  fetchCompanies() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Company[];
        errMessage: string;
      }>(this.apiUrl + 'GetCompaniesByType/' + this.companyType)
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

  upSertCompany(company: Company) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: Company;
        errMessage: string;
      }>(this.apiUrl + 'UpsertCompany', company, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteCompany(company: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Admin/DeleteCompany';
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, company)
      .pipe(
        // map((response) => {
        //   return response;
        // })
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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
  fetchLicenseTypes(stateProv: string | null = null) {
    const queryParams = `?vStateAbv=${stateProv ? stateProv : ''}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetLicenseTypes/${queryParams}`)
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
    const queryParams = `?stateAbv=${stateAbv ? stateAbv : ''}&licenseType=${
      licenseType ? licenseType : ''
    }`;

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

  upSertEducationRule(educationRule: EducationRule) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: EducationRule;
        errMessage: string;
      }>(this.apiUrl + 'UpsertEducationRule', educationRule, {
        observe: 'response',
      })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  disableEducationRule(educationRule: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DisableEducationRule', educationRule)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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
      }>(environment.apiUrl + 'Misc/GetDropdownListTypes')
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
      }>(environment.apiUrl + 'Misc/GetDropdownByType/' + dropdownListType)
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

  upSertLkpType(lkpType: any) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'UpsertLkpType', lkpType, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLkpType(lkpType: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLkpType', lkpType)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  upSertExamItem(examItem: Exam) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: Exam;
        errMessage: string;
      }>(this.apiUrl + 'UpsertExam', examItem, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteExamItem(examItem: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteExam', examItem)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  //   filterJobTitleData(
  //     filterJobTitle: string | null = null,
  //     isActive: boolean | null = null,
  //     licLevel: string | null = null,
  //     licIncentive: string | null = null
  //   ) {

  // console.log('EMFTEST (filterJobTitleData) - isActive: ', isActive);
  // console.log('EMFTEST (filterJobTitleData) - this.jobTitles => \n ', this.jobTitles);

  //     if (
  //       filterJobTitle === null &&
  //       isActive === null &&
  //       licLevel === null &&
  //       licIncentive === null
  //     ) {
  //       this.jobTitlesChanged.next(this.jobTitles);
  //       return;
  //     }

  //     let filteredJobTitles: any[] = this.jobTitles.slice();

  //     if (filterJobTitle !== null) {
  //       filteredJobTitles = filteredJobTitles.filter((jobTitle) =>
  //         jobTitle.jobTitle1.toLowerCase().includes(filterJobTitle.toLowerCase())
  //       );
  //       // this.jobTitles = filteredJobTitles;
  //       // this.jobTitlesChanged.next(this.jobTitles);
  //     }

  //     if (isActive !== null) {
  //       filteredJobTitles = filteredJobTitles.filter(
  //         (jobTitle) => jobTitle.isActive === isActive
  //       );
  //       // this.jobTitles = filteredJobTitles;
  //       // this.jobTitlesChanged.next(this.jobTitles);
  //     }

  //     if (licLevel !== null) {
  //       filteredJobTitles = filteredJobTitles.filter(
  //         (jobTitle) => jobTitle.licLevel === licLevel
  //       );
  //       // this.jobTitles = filteredJobTitles;
  //       // this.jobTitlesChanged.next(this.jobTitles);
  //     }

  //     if (licIncentive !== null) {
  //       filteredJobTitles = filteredJobTitles.filter(
  //         (jobTitle) => jobTitle.licIncentive === licIncentive
  //       );
  //       // this.jobTitles = filteredJobTitles;
  //       // this.jobTitlesChanged.next(this.jobTitles);
  //     }

  //     this.jobTitles = filteredJobTitles;
  //     this.jobTitlesChanged.next(filteredJobTitles);
  //   }
  filterJobTitleData(
    filterJobTitle: string | null = null,
    isActive: boolean | null = null,
    licLevel: string | null = null,
    licIncentive: string | null = null
  ) {
    // console.log('EMFTEST (filterJobTitleData) - isActive: ', isActive);
    // console.log('EMFTEST (filterJobTitleData) - licLevel => \n ', licLevel);
    // console.log('EMFTEST (filterJobTitleData) - licIncentive => \n ', licIncentive);

    if (
      filterJobTitle === null &&
      isActive === null &&
      licLevel === null &&
      licIncentive === null
    ) {
      const jobTitlesToEmit =
        isActive !== null
          ? this.jobTitles.filter((jobTitle) => jobTitle.isActive === isActive)
          : this.jobTitles;
      this.jobTitlesFilterChanged.next(jobTitlesToEmit);
      return;
    }

    const filteredJobTitles = this.jobTitles.filter((jobTitle) => {
      const filterJobTitleMatch =
        filterJobTitle === null
          ? true
          : jobTitle.jobTitle1
              .toLowerCase()
              .includes(filterJobTitle.toLowerCase());
      const isActiveMatch =
        isActive === null ? true : jobTitle.isActive === isActive;
      const licLevelMatch =
        licLevel === null ? true : jobTitle.licenseLevel === licLevel;
      const licIncentiveMatch =
        licIncentive === null
          ? true
          : jobTitle.licenseIncentive === licIncentive;

      return (
        filterJobTitleMatch &&
        isActiveMatch &&
        licLevelMatch &&
        licIncentiveMatch
      );
    });

    this.jobTitlesFilter = filteredJobTitles;
    this.jobTitlesFilterChanged.next(filteredJobTitles);
  }

  upsertJobTitle(jobTitle: JobTitle) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: JobTitle;
        errMessage: string;
      }>(this.apiUrl + 'UpsertJobTitle', jobTitle, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
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

  upsertLicenseItem(licenseItem: License) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: License;
        errMessage: string;
      }>(this.apiUrl + 'UpsertLicense', licenseItem, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLicenseItem(licenseItem: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLicense', licenseItem)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
        })
      );
  }
  // //-> LICENSE-COMPANY-ITEM
  addLicenseCompany(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: License;
        errMessage: string;
      }>(this.apiUrl + 'AddLicenseCompany', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  updateLicenseCompany(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'UpdateLicenseCompany', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLicenseCompany(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLicenseCompany', item)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            console.error(error);
            // throw error;
          },
        })
      );
  }
  // //-> LICENSE-EXAM-ITEM
  addLicenseExam(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: License;
        errMessage: string;
      }>(this.apiUrl + 'AddLicenseExam', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  updateLicenseExam(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'UpdateLicenseExam', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLicenseExam(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLicenseExam', item)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            console.error(error);
            // throw error;
          },
        })
      );
  }
  // //-> LICENSE-EDUCATION-ITEM
  addLicenseEducation(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: License;
        errMessage: string;
      }>(this.apiUrl + 'AddLicensePreEducation', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  updateLicenseEducation(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'UpdateLicensePreEducation', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLicenseEducation(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLicensePreEducation', item)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            console.error(error);
            // throw error;
          },
        })
      );
  }
  // //-> LICENSE-PRODUCT-ITEM
  addLicenseProduct(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: License;
        errMessage: string;
      }>(this.apiUrl + 'AddLicenseProduct', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  updateLicenseProduct(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'UpdateLicenseProduct', item)
      .pipe(
        tap({
          next: (response) => {
            // console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLicenseProduct(item: any) {
    this.licenseID = item.licenseID;
    this.licenseIdChanged.next(this.licenseID);
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLicenseProduct', item)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            console.error(error);
            // throw error;
          },
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

  upSertLicenseTech(licenseTech: LicenseTech) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: LicenseTech;
        errMessage: string;
      }>(this.apiUrl + 'UpsertLicenseTech', licenseTech, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteLicenseTech(licenseTech: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteLicenseTech', licenseTech)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  upsertPreEducationItem(preEducation: PreEducation) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: PreEducation;
        errMessage: string;
      }>(this.apiUrl + 'UpsertPreEducation', preEducation, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deletePreEducationItem(preEducation: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeletePreEducation', preEducation)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  upsertProduct(product: Product) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: Product;
        errMessage: string;
      }>(this.apiUrl + 'UpsertProduct', product, { observe: 'response' })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteProduct(product: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteProduct', product)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  upSertStateRequirement(stateRequirement: StateRequirement) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: StateRequirement;
        errMessage: string;
      }>(this.apiUrl + 'UpsertRequiredLicense', stateRequirement, {
        observe: 'response',
      })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteStateRequirement(stateRequirement: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteRequiredLicense', stateRequirement)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  upSertStateProvince(stateProvince: StateProvince) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: StateProvince;
        errMessage: string;
      }>(this.apiUrl + 'UpsertStateProvince', stateProvince, {
        observe: 'response',
      })
      .pipe(
        tap({
          next: (response) => {
            console.log(response.status);
          },
          error: (error) => {
            console.error(error);
          },
        })
      );
  }

  deleteStateProvince(stateProvince: any) {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'DeleteStateProvince', stateProvince)
      .pipe(
        tap({
          next: (response) => {
            if (response.success && response.statusCode === 200) {
              // return response;
            } else {
              throw new Error(response.errMessage || 'Unknown error');
            }
          },
          error: (error) => {
            // console.error(error);
            // throw error;
          },
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

  storeCompanyItem(
    mode: string | '',
    companyItem: any | null,
    licenseID: number | null = null
  ) {
    this.adminComService.changeMode('companyItem', mode);
    this.companyItem = companyItem || {};
    this.companyItemChanged.next(this.companyItem);
    this.licenseID = licenseID || 0;
    this.licenseIdChanged.next(this.licenseID);
  }

  storePreExamItem(
    mode: string | '',
    preExamItem: any | null,
    licenseID: number | null = null
  ) {
    this.adminComService.changeMode('preExamItem', mode);
    this.preExamItem = preExamItem || {};
    this.preExamItemChanged.next(this.preExamItem);
    this.licenseID = licenseID || 0;
    this.licenseIdChanged.next(this.licenseID);
  }

  storePreEduItem(
    mode: string | '',
    preEduItem: any | null,
    licenseID: number | null = null
  ) {
    this.adminComService.changeMode('preEduItem', mode);
    this.preEduItem = preEduItem || {};
    this.preEduItemChanged.next(this.preEduItem);
    this.licenseID = licenseID || 0;
    this.licenseIdChanged.next(this.licenseID);
  }

  storeProductItem(
    mode: string | '',
    productItem: any | null,
    licenseID: number | null = null
  ) {
    this.adminComService.changeMode('productItem', mode);
    this.productItem = productItem || {};
    this.productItemChanged.next(this.productItem);
    this.licenseID = licenseID || 0;
    this.licenseIdChanged.next(this.licenseID);
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
