export interface AgentLicApplicationInfo {
  licenseApplicationItems: LicenseApplicationItem[];
  licensePreEducationItems: LicensePreEducationItem[];
  licensePreExamItems: LicensePreExamItem[];
  licenseRenewalItems: LicenseRenewalItem[];
}

export interface LicenseApplicationItem {
  licenseApplicationID: number;
  sentToAgentDate: string;
  recFromAgentDate: string | null;
  sentToStateDate: string | null;
  recFromStateDate: string | null;
  applicationStatus: string;
  applicationType: string;
}

export interface LicensePreEducationItem {
  employeeLicensePreEducationID: number;
  status: string;
  educationStartDate: string;
  educationEndDate: string;
  preEducationID: number;
  companyID: number | null;
  educationName: string;
  employeeLicenseID: number;
  additionalNotes: string | null;
}

export interface LicensePreExamItem {
  employeeLicensePreExamID: number;
  employeeLicenseID: number;
  status: string;
  examID: number;
  examName: string;
  examScheduleDate: string;
  examTakenDate: string | null;
  additionalNotes: string | null;
}

export interface LicenseRenewalItem {
  employeeLicenseID: number;
  licenseApplicationID: number;
  sentToAgentDate: string;
  recFromAgentDate: string;
  sentToStateDate: string;
  recFromStateDate: string | null;
  applicationStatus: string;
  applicationType: string;
  renewalDate: string;
  renewalMethod: string;
}
