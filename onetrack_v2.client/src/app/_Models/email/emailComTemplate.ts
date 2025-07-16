export interface EmailComTemplate {
    communicationID: number;
    communicationName: string;
    docType: string;
    docTypeAbv: string;
    docSubType: string;
    emailAttachments: string;
    hasNote: boolean;
    docTypeDocSubType: string;
  }

  export interface SendEmailData {
    employeeID: number;
    employmentID: number;
    communicationID: number;
    emailTo: string;
    ccEmail?: string[]; // Optional
    emailSubject: string;
    emailContent: string;
    fileAttachments?: File[]; // Optional
    userSOEID: string;
  }

  export interface SendIncentiveEmail {
    employeeID: number;
    employmentID: number;
    employeeLicenseID: number;
    incentiveID?: number; // Optional (nullable in C#)
    typeOfIncentive?: string; // Optional with default empty string
    incentiveEmailType?: string; // Optional with default empty string
    branchMgrEmploymentID?: number; // Optional (nullable in C#)
    distMgrEmploymentID?: number; // Optional (nullable in C#)
    userSOEID: string; // Required with default empty string
  }