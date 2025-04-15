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