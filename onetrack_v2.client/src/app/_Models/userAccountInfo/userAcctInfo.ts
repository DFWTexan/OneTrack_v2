export interface UserAcctInfo {
    licenseTechID: number | null;
    displayName: string;
    soeid: string;
    email: string | null;
    enabled: boolean | null;
    employeeId: string | null;
    homeDirectory: string | null;
    lastLogon: string | null;
    isAdminRole: boolean | null;
    isTechRole: boolean | null;
    isReadRole: boolean | null;
    isQARole: boolean | null;
    isSuperUser: boolean | null;
  }
  