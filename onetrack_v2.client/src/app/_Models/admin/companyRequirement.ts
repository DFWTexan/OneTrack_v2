export interface CompanyRequirement {
  companyRequirementId: number | null;
  workStateAbv: string | null;
  resStateAbv: string | null;
  requirementType: string | null;
  licLevel1: boolean | null;
  licLevel2: boolean | null;
  licLevel3: boolean | null;
  licLevel4: boolean | null;
  startAfterDate: string | null;
  document: string | null;
  userSOEID: string | null;
}
