import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Subject } from 'rxjs';

// import { environment } from '../../_environments/environment';
import { ConfigService } from '../config/config.service';

@Injectable({
  providedIn: 'root',
})
export class DropdownDataService {
  public branchNames: { value: string; label: string }[] = [];
  public rollOutGroups: { value: string; label: string }[] = [];
  rollOutGroupsChanged = new Subject<{ value: string; label: string }[]>();
  branchNamesChanged = new Subject<{ value: string; label: string }[]>();
  public scoreNumbers: { value: number; label: string }[] = [];
  scoreNumbersChanged = new Subject<{ value: number; label: string }[]>();
  public employerAgencies: { value: number; label: string }[] = [];
  employerAgenciesChanged = new Subject<{ value: number; label: string }[]>();
  public licenseStatuses: { value: number; label: string }[] = [];
  licenseStatusesChanged = new Subject<{ value: number; label: string }[]>();
  public licenseNames: { value: number; label: string }[] = [];
  licenseNamesChanged = new Subject<{ value: number; label: string }[]>();
  public jobTitles: { value: number; label: string }[] = [];
  jobTitlesChanged = new Subject<{ value: number; label: string }[]>();
  public conEduStartDateItems: { value: number; label: string }[] = [];
  conEduStartDateItemsChanged = new Subject<
    { value: number; label: string }[]
  >();
  public conEduEndDateItems: { value: number; label: string }[] = [];
  conEduEndtDateItemsChanged = new Subject<
    { value: number; label: string }[]
  >();
  public conEduExceptions: { value: number; label: string }[] = [];
  conEduExceptionsChanged = new Subject<{ value: number; label: string }[]>();
  public conEduExemptions: { value: number; label: string }[] = [];
  conEduExemptionsChanged = new Subject<{ value: number; label: string }[]>();
  public lineOfAuthorities: { value: number; label: string }[] = [];
  lineOfAuthoritiesChanged = new Subject<{ value: number; label: string }[]>();
  public documentTypes: string[] = [];
  documentTypesChanged = new Subject<string[]>();
  public licenseTechs: { value: any; label: string }[] = [];
  licenseTechsChanged = new Subject<{ value: any; label: string }[]>();

  // private url: string = environment.apiUrl + 'Misc/';

  constructor(private http: HttpClient, private configService: ConfigService) {}

  fetchDropdownData(vEndpoint: string) {
    const url = this.configService.config.apiUrl + 'Misc/' + vEndpoint;
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: { value: string; label: string }[];
        errMessage: string;
      }>(url)
      .pipe(
        map((response) => {
          // Check if the response is successful and has data
          if (response.success && response.objData) {
            // Map the objData to the desired format
            return response.objData.map((item) => ({
              value: item.value, // Assuming you want to map 'key' to 'value'
              label: item.label, // 'value' is mapped to 'label'
            }));
          } else {
            // Handle the case where response is not successful or objData is not available
            // This can be adjusted based on how you want to handle errors or empty data
            return [];
          }
        })
      );
  }

  fetchDropdownNumberValueData(vEndpoint: string) {
    const url = this.configService.config.apiUrl + 'Misc/' + vEndpoint;
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: { value: number; label: string }[];
        errMessage: string;
      }>(url)
      .pipe(
        map((response) => {
          // Check if the response is successful and has data
          if (response.success && response.objData) {
            // Map the objData to the desired format
            return response.objData.map((item) => ({
              value: item.value, // Assuming you want to map 'key' to 'value'
              label: item.label, // 'value' is mapped to 'label'
            }));
          } else {
            // Handle the case where response is not successful or objData is not available
            // This can be adjusted based on how you want to handle errors or empty data
            return [];
          }
        })
      );
  }

  updateRollOutGroups(rollOutGroups: { value: string; label: string }[]) {
    this.rollOutGroups = rollOutGroups;
    this.rollOutGroupsChanged.next(rollOutGroups);
  }

  updateBranchNames(branchNames: { value: string; label: string }[]) {
    this.branchNames = branchNames;
    this.branchNamesChanged.next([...this.branchNames]);
  }

  updateScoreNumbers(scoreNumbers: { value: number; label: string }[]) {
    this.scoreNumbers = scoreNumbers;
    this.scoreNumbersChanged.next([...this.scoreNumbers]);
  }

  updateEmployerAgencies(employerAgencies: { value: number; label: string }[]) {
    this.employerAgencies = employerAgencies;
    this.employerAgenciesChanged.next([...this.employerAgencies]);
  }

  updateLicenseStatuses(licenseStatuses: { value: number; label: string }[]) {
    this.licenseStatuses = licenseStatuses;
    this.licenseStatusesChanged.next([...this.licenseStatuses]);
  }

  updateLicenseNames(licenseNames: { value: number; label: string }[]) {
    this.licenseNames = licenseNames;
    this.licenseNamesChanged.next([...this.licenseNames]);
  }

  updateJobTitles(jobTitles: { value: number; label: string }[]) {
    this.jobTitles = jobTitles;
    this.jobTitlesChanged.next([...this.jobTitles]);
  }

  updateConEduStartDateItems(
    conEduStartDateItems: { value: number; label: string }[]
  ) {
    this.conEduStartDateItems = conEduStartDateItems;
    this.conEduStartDateItemsChanged.next([...this.conEduStartDateItems]);
  }

  updateConEduEndDateItems(
    conEduEndDateItems: { value: number; label: string }[]
  ) {
    this.conEduEndDateItems = conEduEndDateItems;
    this.conEduEndtDateItemsChanged.next([...this.conEduEndDateItems]);
  }

  updateConEduExceptions(conEduExceptions: { value: number; label: string }[]) {
    this.conEduExceptions = conEduExceptions;
    this.conEduExceptionsChanged.next([...this.conEduExceptions]);
  }

  updateConEduExemptions(conEduExemptions: { value: number; label: string }[]) {
    this.conEduExemptions = conEduExemptions;
    this.conEduExemptionsChanged.next([...this.conEduExemptions]);
  }

  updateLineOfAuthorities(
    lineOfAuthorities: { value: number; label: string }[]
  ) {
    this.lineOfAuthorities = lineOfAuthorities;
    this.lineOfAuthoritiesChanged.next([...this.lineOfAuthorities]);
  }

  updateDocumentTypes(documentTypes: string[]) {
    this.documentTypes = documentTypes;
    this.documentTypesChanged.next([...this.documentTypes]);
  }

  updateLicenseTechs(licenseTechs: { value: any; label: string }[]) {
    this.licenseTechs = licenseTechs;
    this.licenseTechsChanged.next([...this.licenseTechs]);
  }

  fetchDropdownNumericData(vEndpoint: string, vValue: any | null = null) {
    const url = this.configService.config.apiUrl + 'Misc/' + vEndpoint + (vValue ? '/' + vValue : '');
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: { value: number; label: string }[];
        errMessage: string;
      }>(url)
      .pipe(
        map((response) => {
          // Check if the response is successful and has data
          if (response.success && response.objData) {
            // Map the objData to the desired format
            return response.objData.map((item) => ({
              value: item.value, // Assuming you want to map 'key' to 'value'
              label: item.label, // 'value' is mapped to 'label'
            }));
          } else {
            // Handle the case where response is not successful or objData is not available
            // This can be adjusted based on how you want to handle errors or empty data
            return [];
          }
        })
      );
  }
}
