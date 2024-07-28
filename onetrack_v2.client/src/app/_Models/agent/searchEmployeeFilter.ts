export interface SearchEmployeeFilter {
  EmployeeSSN: string | null;
  TeamMemberGEID: string | null;
  NationalProducerNumber: number | 0;
  LastName: string | null;
  FirstName: string | null;
  ResState: string | null;
  WrkState: string | null;
  BranchCode: string | null;
  AgentStatus: string[] | ["All"];
  ScoreNumber: string | null;
  CompanyID: number | null;
  LicStatus: string[] | ['All'];
  LicState: string | null;
  LicenseName: string | null;
}
