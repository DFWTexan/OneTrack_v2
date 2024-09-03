export interface UserAcctInfo {
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
    isSuperUser: boolean | null;
  }
  