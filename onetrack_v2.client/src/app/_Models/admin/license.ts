export interface License {
  licenseId: number;
  licenseName: string;
  licenseAbv: string | null;
  stateProvinceAbv: string;
  lineOfAuthorityAbv: string;
  lineOfAuthorityId: number;
  agentStateTable: boolean;
  plsIncentive1Tmpay: number;
  plsIncentive1Mrpay: number;
  incentive2PlusTmpay: number;
  incentive2PlusMrpay: number;
  licIncentive3Tmpay: number;
  licIncentive3Mrpay: number;
  isActive: boolean;
  companyItems: CompanyItem[];
  preExamItems: PreExamItem[];
  preEducationItems: PreEduItem[];
  productItems: ProductItem[];
}

export interface CompanyItem {
  licenseID: number;
  licenseCompanyId: number;
  companyId: number;
  companyAbv: string;
  companyType: string;
  companyName: string;
  tin: string | null;
  naicNumber: string | null;
  isActive: boolean;
  addressId: number;
  address1: string | null;
  address2: string | null;
  city: string | null;
  state: string;
  zip: string | null;
  phone: string | null;
  fax: string | null;
  country: string;
}

export interface PreExamItem {
  licenseID: number;
  examId: number;
  examName: string | null;
  stateProvinceAbv: string | null;
  companyName: string | null;
  deliveryMethod: string | null;
  licenseExamID: number;
  examProviderID: number;
  isActive: boolean | null;
}

export interface PreEduItem {
  licenseID: number;
  licensePreEducationID: number;
  preEducationID: number;
  educationName: string | null;
  stateProvinceAbv: string | null;
  creditHours: number;
  companyID: number;
  companyName: string | null;
  deliveryMethod: string | null;
  isActive: boolean | null;
}

export interface ProductItem {
  licenseID: number;
  licenseProductID: number;
  productID: number;
  productName: string | null;
  productAbv: string | null;
  isActive: boolean | null;
}