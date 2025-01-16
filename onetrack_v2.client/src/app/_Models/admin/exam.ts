export interface Exam {
  examId: number;
  examName: string;
  examFees: number | null;
  stateProvinceAbv: string;
  examProviderId: number | null;
  companyName: string | null;
  deliveryMethod: string | null;
}
