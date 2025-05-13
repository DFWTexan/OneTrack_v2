import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { environment } from '../../_environments/environment';
import { LicenseAppointment, LicenseIncentiveInfo } from '../../_Models';
import { AgentDataService } from '../agent/agent.data.service';
import { ErrorMessageService } from '../error/error.message.service';

@Injectable({
  providedIn: 'root',
})
export class LicIncentiveInfoDataService {
  private apiUrl: string = environment.apiUrl + 'LicenseInfo/';
  licenseIncentiveInfo: LicenseIncentiveInfo = {} as LicenseIncentiveInfo;
  licenseIncentiveInfoChanged = new Subject<LicenseIncentiveInfo[]>();

  // DATA
  dmManagers: any[] = [];
  dmManagersChanged = new Subject<any[]>();
  bmManagers: any[] = [];
  bmManagersChanged = new Subject<any[]>();
  licenseTeches: any[] = [];
  licenseTechesChanged = new Subject<any[]>();

  constructor(
    private http: HttpClient,
    private agentDataService: AgentDataService
  ) {}

  fetchLicIncentiveInfo(
    employeeLicenseId: number
  ): Observable<LicenseIncentiveInfo> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetIncentiveInfo/' + employeeLicenseId)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseIncentiveInfo = response.objData;
            this.licenseIncentiveInfoChanged.next([this.licenseIncentiveInfo]);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchDMManagers(): Observable<any> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetIncentiveDMMrgs')
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

  updateDMManagers(dmManagers: any) {
    this.dmManagers = dmManagers;
    this.dmManagersChanged.next(this.dmManagers);
  }

  fetchBMManagers(): Observable<any> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetIncentiveBMMgrs')
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

  updateBMManagers(bmManagers: any) {
    this.bmManagers = bmManagers;
    this.bmManagersChanged.next(this.bmManagers);
  }

  fetchLicenseTeches(): Observable<any> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetIncentiveTechNames')
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

  updateLicenseTeches(licenseTeches: any) {
    this.licenseTeches = licenseTeches;
    this.licenseTechesChanged.next(this.licenseTeches);
  }

  // addLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/AddLicenseAppointment';

  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, appointment)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentLicenseAppointments(
  //             this.agentDataService.agentInformation.employmentID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((appointments) => {
  //         // this.agentDataService.agentInformation.agentLicenseAppointments =
  //         //   appointments;
  //         // this.agentDataService.agentLicenseAppointmentsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicenseAppointments
  //         // );
  //         return appointments;
  //       })
  //     );
  // }
  addLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/AddLicenseAppointment`;
  
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, appointment)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in addLicenseAppointment:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // updateLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/UpdateLicenseAppointment';
  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, appointment)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentLicenseAppointments(
  //             this.agentDataService.agentInformation.employmentID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((appointments) => {
  //         // this.agentDataService.agentInformation.agentLicenseAppointments =
  //         //   appointments;
  //         // this.agentDataService.agentLicenseAppointmentsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicenseAppointments
  //         // );

  //         // this.agentDataService.updateAgentLicenseAppointments(appointments);
  //         return appointments;
  //       })
  //     );
  // }
  updateLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/UpdateLicenseAppointment`;

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, appointment)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in updateLicenseAppointment:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // upsertLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/UpsertLicenseAppointment';
  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, appointment)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentLicenseAppointments(
  //             this.agentDataService.agentInformation.employmentID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((appointments) => {
  //         // this.agentDataService.agentInformation.agentLicenseAppointments =
  //         //   appointments;
  //         // this.agentDataService.agentLicenseAppointmentsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicenseAppointments
  //         // );

  //         // this.agentDataService.updateAgentLicenseAppointments(appointments);
  //         return appointments;
  //       })
  //     );
  // }
  upsertLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/UpsertLicenseAppointment`;

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, appointment)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in upsertLicenseAppointment:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // deleteLicenseAppointment(appointment: any): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/DeleteLicenseAppointment';

  //   return this.http
  //     .put<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, appointment)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentLicenseAppointments(
  //             this.agentDataService.agentInformation.employmentID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((appointments) => {
  //         // this.agentDataService.agentInformation.agentLicenseAppointments =
  //         //   appointments;
  //         // this.agentDataService.agentLicenseAppointmentsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicenseAppointments
  //         // );

  //         // this.agentDataService.updateAgentLicenseAppointments(appointments);
  //         return appointments;
  //       })
  //     );
  // }
  deleteLicenseAppointment(appointment: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/DeleteLicenseAppointment`;

    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, appointment)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in deleteLicenseAppointment:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // upsertLicenseApplicationItem(licApplicationItem: any): Observable<any> {
  //   const apiUrl = `${environment.apiUrl}LicenseInfo/UpsertLicenseApplication`;

  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(apiUrl, licApplicationItem)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentInformation(
  //             this.agentDataService.agentInformation.employeeID
  //           );
  //         } else {
  //           return throwError(
  //             () => new Error(response.errMessage || 'Unknown error')
  //           );
  //         }
  //       }),
  //       map((licApplications) => licApplications), // Pass through the data
  //       catchError((error) => {
  //         console.error('Error in upsertLicenseApplicationItem:', error);
  //         return throwError(() => error); // Re-throw the error for the caller to handle
  //       })
  //     );
  // }
  // upsertLicenseApplicationItem(licApplicationItem: any): Observable<any> {
  //   const apiUrl = `${environment.apiUrl}LicenseInfo/UpsertLicenseApplication`;

  //   // Ensure nullable fields are set to null instead of empty strings
  //   const sanitizedPayload = {
  //     ...licApplicationItem,
  //     recFromStateDate: licApplicationItem.recFromStateDate || null,
  //     renewalDate: licApplicationItem.renewalDate || null,
  //     sentToStateDate: licApplicationItem.sentToStateDate || null,
  //     renewalMethod: licApplicationItem.renewalMethod || null,
  //   };

  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(apiUrl, sanitizedPayload)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentInformation(
  //             this.agentDataService.agentInformation.employeeID
  //           );
  //         } else {
  //           return throwError(() => new Error(response.errMessage || 'Unknown error'));
  //         }
  //       }),
  //       map((licApplications) => licApplications),
  //       catchError((error) => {
  //         console.error('Error in upsertLicenseApplicationItem:', error);
  //         return throwError(() => error);
  //       })
  //     );
  // }
  upsertLicenseApplicationItem(licApplicationItem: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/UpsertLicenseApplication`;

    // Ensure nullable fields are set to null instead of empty strings
    const sanitizedPayload = {
      ...licApplicationItem,
      recFromAgentDate: licApplicationItem.recFromAgentDate || null,
      recFromStateDate: licApplicationItem.recFromStateDate || null,
      renewalDate: licApplicationItem.renewalDate || null,
      sentToStateDate: licApplicationItem.sentToStateDate || null,
      renewalMethod: licApplicationItem.renewalMethod || null,
    };

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, sanitizedPayload)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in upsertLicenseApplicationItem:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // deleteLicenseApplicationItem(licApplicationItem: any): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/DeleteLicenseApplication';

  //   return this.http
  //     .put<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, licApplicationItem)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentInformation(
  //             this.agentDataService.agentInformation.employeeID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((licApplications) => {
  //         // this.agentDataService.agentInformation.agentLicenseApplications =
  //         //   licApplications;
  //         // this.agentDataService.agentLicenseApplicationsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicenseApplications
  //         // );

  //         // this.agentDataService.updateAgentLicenseApplications(licApplications);
  //         return licApplications;
  //       })
  //     );
  // }
  deleteLicenseApplicationItem(licApplicationItem: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/DeleteLicenseApplication`;

    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, licApplicationItem)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in deleteLicenseApplicationItem:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // upsertLicensePreEducationItem(licPreEduItem: any): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/UpsertLicensePreEducation';

  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, licPreEduItem)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentInformation(
  //             this.agentDataService.agentInformation.employeeID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((licPreEdus) => {
  //         // this.agentDataService.agentInformation.agentLicensePreEducations =
  //         //   licPreEdus;
  //         // this.agentDataService.agentLicensePreEducationsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicensePreEducations
  //         // );

  //         // this.agentDataService.updateAgentLicensePreEducations(licPreEdus);
  //         return licPreEdus;
  //       })
  //     );
  // }
  upsertLicensePreEducationItem(licPreEduItem: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/UpsertLicensePreEducation`;

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, licPreEduItem)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in upsertLicensePreEducationItem:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  // deleteLicensePreEducationItem(licPreEduItem: any): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/DeleteLicensePreEducation';

  //   return this.http
  //     .put<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, licPreEduItem)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.agentDataService.fetchAgentInformation(
  //             this.agentDataService.agentInformation.employeeID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((licPreEdus) => {
  //         // this.agentDataService.agentInformation.agentLicensePreEducations =
  //         //   licPreEdus;
  //         // this.agentDataService.agentLicensePreEducationsChanged.next(
  //         //   this.agentDataService.agentInformation.agentLicensePreEducations
  //         // );

  //         // this.agentDataService.updateAgentLicensePreEducations(licPreEdus);
  //         return licPreEdus;
  //       })
  //     );
  // }
  deleteLicensePreEducationItem(licPreEduItem: any): Observable<any> {
    const apiUrl = `${environment.apiUrl}LicenseInfo/DeleteLicensePreEducation`;

    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(apiUrl, licPreEduItem)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData; // Return the response data if successful
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        catchError((error) => {
          console.error('Error in deleteLicensePreEducationItem:', error);
          return throwError(() => error); // Re-throw the error for the caller to handle
        })
      );
  }

  upsertLicensePreExamItem(licPreExamItem: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'LicenseInfo/UpsertLicensePreExam';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, licPreExamItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.agentDataService.fetchAgentInformation(
              this.agentDataService.agentInformation.employeeID
            );
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((licPreExams) => {
          // this.agentDataService.agentInformation.agentLicensePreExams =
          //   licPreExams;
          // this.agentDataService.agentLicensePreExamsChanged.next(
          //   this.agentDataService.agentInformation.agentLicensePreExams
          // );

          // this.agentDataService.updateAgentLicensePreExams(licPreExams);
          return licPreExams;
        })
      );
  }

  deleteLicensePreExamItem(licPreExamItem: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'LicenseInfo/DeleteLicensePreExam';

    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, licPreExamItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.agentDataService.fetchAgentInformation(
              this.agentDataService.agentInformation.employeeID
            );
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((licPreExams) => {
          // this.agentDataService.agentInformation.agentLicensePreExams =
          //   licPreExams;
          // this.agentDataService.agentLicensePreExamsChanged.next(
          //   this.agentDataService.agentInformation.agentLicensePreExams
          // );

          // this.agentDataService.updateAgentLicensePreExams(licPreExams);
          return licPreExams;
        })
      );
  }

  // updateLicenseIncentiveInfo(licenseIncentiveInfo: any) {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/UpdateLicenseIncentive';
  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, licenseIncentiveInfo)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.fetchLicIncentiveInfo(
  //             licenseIncentiveInfo.employeeLicenseID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       catchError((error) => {
  //         console.error('EMFTEST (ERROR) - Server error: ', error);
  //         // if (error.error && error.error.errMessage) {
  //         //   this.errorMessageService.setErrorMessage(error.error.errMessage);
  //         // }
  //         throw error;
  //       })
  //     );
  // }
  // updateLicenseIncentiveInfo(licenseIncentiveInfo: any) {
  //   this.apiUrl = environment.apiUrl + 'LicenseInfo/UpdateLicenseIncentive';
  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, licenseIncentiveInfo)
  //     .pipe(
  //       map((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return response;
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       catchError((error) => {
  //         console.error('EMFTEST (ERROR) - Server error: ', error);
  //         throw error;
  //       })
  //     );
  // }
  // updateLicenseIncentiveInfo(
  //   licenseIncentiveInfo: LicenseIncentiveInfo
  // ): Promise<any> {
  //   const apiUrl = `${environment.apiUrl}LicenseInfo/UpdateLicenseIncentive`;

  //   console.log(
  //     'EMFTEST (updateLicenseIncentiveInfo) - licenseIncentiveInfo => \n ',
  //     licenseIncentiveInfo
  //   );

  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(apiUrl, licenseIncentiveInfo)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.fetchLicIncentiveInfo(
  //             licenseIncentiveInfo.employeeLicenseID
  //           );
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       catchError((error) => {
  //         console.error('EMFTEST (ERROR) - Server error: ', error);
  //         throw error;
  //       })
  //     )
  //     .toPromise();
  // }
  updateLicenseIncentiveInfo(
  licenseIncentiveInfo: LicenseIncentiveInfo
): Promise<any> {
  const apiUrl = `${environment.apiUrl}LicenseInfo/UpdateLicenseIncentive`;

  console.log(
    'EMFTEST (updateLicenseIncentiveInfo) - licenseIncentiveInfo => \n ',
    licenseIncentiveInfo
  );

  return this.http
    .post<{
      success: boolean;
      statusCode: number;
      objData: any;
      errMessage: string;
    }>(apiUrl, licenseIncentiveInfo)
    .pipe(
      map((response) => {
        if (response.success && response.statusCode === 200) {
          return response.objData; // Return the response data if successful
        } else {
          throw new Error(response.errMessage || 'Unknown error');
        }
      }),
      catchError((error) => {
        console.error('EMFTEST (ERROR) - Server error: ', error);
        throw error;
      })
    )
    .toPromise();
    // .toPromise()
    // .then((response) => {
    //   // Fetch updated license incentive info after the update
    //   return this.fetchLicIncentiveInfo(licenseIncentiveInfo.employeeLicenseID)
    //     .toPromise()
    //     .then((updatedInfo) => updatedInfo)
    //     .catch((fetchError) => {
    //       console.error('Error fetching updated license incentive info:', fetchError);
    //       throw fetchError;
    //     });
    // });
}
}
