export interface UserAcctInfo {
    displayName: string;
    userSamAcctName: string;
    email: string;
    enabled: boolean;
    employeeId: string;
    homeDirectory: string;
    lastLogon: string;
    isAdminRole: boolean;
    isTechRole: boolean | null;
    isReadRole: boolean;
    isSuperUser: boolean;
  }
  