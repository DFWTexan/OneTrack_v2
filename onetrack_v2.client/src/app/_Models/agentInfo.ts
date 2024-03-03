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
}
