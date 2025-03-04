export interface LicenseInfo {
    employeeID: number;
    employmentID: number;
    ascEmployeeLicenseID: number | null;
    licenseID: number;
    licenseExpireDate: string | null;
    licenseState: string;
    licenseStatus: string;
    licenseNumber: string;
    reinstatement: boolean | null;
    required: boolean | null;
    nonResident: boolean | null;
    licenseEffectiveDate: string | null;
    licenseIssueDate: string | null;
    lineOfAuthorityIssueDate: string | null;
    sentToAgentDate: string | null;
    licenseNote: string | null;
    employeeLicenseId: number;
    appointmentStatus: string | null;
    companyID: number | null;
    carrierDate: string | null;
    appointmentEffectiveDate: string | null;
    appointmentExpireDate: string | null;
    appointmentTerminationDate: string | null;
    UserSOEID: string;
  }