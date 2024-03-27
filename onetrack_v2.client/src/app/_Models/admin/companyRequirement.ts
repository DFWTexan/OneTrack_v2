export interface CompanyRequirement {
  companyRequirementId: number;
  workStateAbv: string;
  resStateAbv: string;
  requirementType: string;
  licLevel1: boolean;
  licLevel2: boolean;
  licLevel3: boolean;
  licLevel4: boolean;
  startAfterDate: string;
  document: string;
}
