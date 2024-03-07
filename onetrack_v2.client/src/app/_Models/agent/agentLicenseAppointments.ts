export interface AgentLicenseAppointments {
    licenseAppointments: LicenseAppointment[];
    employeeLicenseId: number;
    licenseState: string;
    lineOfAuthority: string;
    licenseStatus: string;
    employmentID: number;
    licenseName: string;
    licenseNumber: string | null;
    resNoneRes: string | null;
    originalIssueDate: string | null;
    lineOfAuthIssueDate: string | null;
    licenseEffectiveDate: string | null;
    licenseExpirationDate: string | null;
  }

  export interface LicenseAppointment {
    licenseID: number;
    employeeAppointmentID: number;
    appointmentEffectiveDate: string | null;
    appointmentStatus: string;
    employeeLicenseID: number;
    carrierDate: string | null;
    appointmentExpireDate: string | null;
    appointmentTerminationDate: string | null;
    companyID: number;
    retentionDate: string | null;
  }