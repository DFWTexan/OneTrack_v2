export interface EducationRule {
    ruleNumber: number;
    stateProvince: string;
    licenseType: string;
    requiredCreditHours: number;
    educationStartDateID: number;
    educationStartDate: string;
    educationEndDateID: number;
    educationEndDate: string;
    exceptionID: string;
    exemptionID: string;
    isActive: boolean;
    userSOEID: string;
  }