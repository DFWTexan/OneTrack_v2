import { AgentLicenseAppointments } from './agentLicenseAppointments';
import { ManagerHierarchy } from './managerHierarchy';

export interface AgentInfo {
  employeeID: number;
  employmentID: number;
  geid: string;
  employeeStatus: string;
  companyID: number;
  companyName: string;
  lastName: string;
  firstName: string;
  middleName: string | null;
  jobTitle: string | null;
  jobDate: string | null;
  employeeSSN: string;
  soeid: string | null;
  address1: string;
  address2: string | null;
  city: string;
  state: string;
  zip: string;
  phone: string | null;
  email: string;
  nationalProdercerNumber: string | null;
  employerAgency: string | null;
  ceRequired: boolean;
  excludeFromReports: boolean;
  teamMemberGEID: string | null;
  diarySOEID: string | null;
  dateOfBirth: string;
  diaryEntryName: string | null;
  diaryEntryDate: string | null;
  diaryNotes: string | null;
  licenseIncentive: string;
  licenseLevel: string;
  isLicenseincentiveSecondChance: boolean;
  branchCode: string | null;
  branchDeptScoreNumber: string | null;
  branchDeptNumber: string | null;
  branchDeptName: string | null;
  branchDeptStreet1: string | null;
  branchDeptStreet2: string | null;
  branchDeptStreetCity: string | null;
  branchDeptStreetState: string | null;
  branchDeptStreetZip: string | null;
  branchDeptPhone: string | null;
  branchDeptFax: string | null;
  mgrHiearchy: ManagerHierarchy[];
  agentLicenseAppointments: AgentLicenseAppointments[];
  employmentHistory: EmploymentHistory[];
  transferHistory: TransferHistory[];
  companyRequirementsHistory: CompanyRequirementsHistory[];
  employmentJobTitleHistory: EmploymentJobTitleHistory[];
  contEduRequiredItems: ContEduRequiredItem[];
  contEduCompletedItems: ContEduCompletedItem[];
  diaryCreatedByItems: DiaryCreatedByItem[];
  diaryItems: DiaryItem[];
  employmentCommunicationItems: EmploymentCommunicationItem[];
}

export interface EmploymentHistory {
  employmentHistoryID: number;
  hireDate: string;
  rehireDate: string | null;
  notifiedTermDate: string | null;
  hrTermDate: string | null;
  hrTermCode: string | null;
  isForCause: boolean;
  backgroundCheckStatus: string;
  backGroundCheckNotes: string;
  isCurrent: boolean;
}

export interface TransferHistory {
  transferHistoryID: number;
  branchCode: string;
  workStateAbv: string;
  resStateAbv: string;
  transferDate: string;
  state: string | null;
  isCurrent: boolean;
}

export interface CompanyRequirementsHistory {
  employmentCompanyRequirementID: number;
  assetIdString: string;
  learningProgramStatus: string;
  learningProgramEnrollmentDate: string | null;
  learningProgramCompletionDate: string;
}

export interface EmploymentJobTitleHistory {
  employmentJobTitleID: number;
  employmentID: number;
  jobTitleDate: string;
  jobCode: string;
  jobTitle: string;
  isCurrent: boolean;
}
export interface ContEduRequiredItem {
  contEducationRequirementID: number;
  educationStartDate: string | null;
  educationEndDate: string | null;
  requiredCreditHours: number | null;
  isExempt: boolean | null;
  employmentID: number | null;
}

export interface ContEduCompletedItem {
  employeeEducationID: number;
  educationName: string | null;
  contEducationRequirementID: number | null;
  contEducationTakenDate: string | null;
  creditHoursTaken: number | null;
  additionalNotes: string | null;
}
export interface DiaryCreatedByItem {
  SOEID: string | null;
  techName: string | null;
}
export interface DiaryItem {
  diaryID: number;
  SOEID: string | null;
  diaryName: string | null;
  diaryDate: string | null;
  notes: string | null;
}
export interface EmploymentCommunicationItem {
  employmentCommunicationID: number;
  letterName: string | null;
  emailCreateDate: string | null;
  emailSentDate: string | null;
}