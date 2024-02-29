export interface SearchEmployee {
    EmployeeSSN: string | null;
    SCORENumber: string | null;
    NationalProducerNumber: number | 0;
    LastName: string | null;
    FirstName: string | null;
    AgentStatus: string[] | ['All'];
    ResState: string | null;
    WrkState: string | null;
    BranchCode: string | null;
    EmployeeLicenseID: number | 0;
    LicStatus: string | null;
    LicState: string | null;
    LicenseName: string | null;
    EmploymentID: number | 0;
  }