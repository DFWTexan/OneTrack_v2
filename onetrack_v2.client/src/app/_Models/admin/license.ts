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
}
