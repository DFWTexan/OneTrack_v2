export interface AgentLicenseAppointments {
    licenseAppointments: LicenseAppointment[];
    employeeLicenseId: number;
    licenseState: string;
    lineOfAuthority: string;
    licenseStatus: string;
    employmentID: number;
    licenseID: number;
    licenseName: string;
    licenseNumber: string | null;
    resNoneRes: string | null;
    originalIssueDate: string | null;
    lineOfAuthIssueDate: string | null;
    licenseEffectiveDate: string | null;
    licenseExpirationDate: string | null;
    ascEmployeeLicenseID: number | null;
    ascLicenseName: string | null;
    nonResident: boolean;
    required: boolean; 
    reinstatement: boolean; 
    licenseNote: string | null;
  }

  export interface LicenseAppointment {
    licenseID: number;
    employeeLicenseId: number | null;
    employeeAppointmentID: number;
    appointmentEffectiveDate: string | null;
    appointmentStatus: string;
    employeeLicenseID: number;
    carrierDate: string | null;
    appointmentExpireDate: string | null;
    appointmentTerminationDate: string | null;
    companyID: number;
    companyAbv: string;
    retentionDate: string | null;
  }