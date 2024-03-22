export interface TicklerInfo {
  ticklerId: number;
  ticklerDate: string;
  ticklerDueDate: string;
  licenseTechId: number;
  employmentId: number;
  employeeLicenseId: number | null;
  employeeId: number;
  ticklerCloseDate: string | null;
  ticklerCloseByLicenseTechId: number | null;
  message: string;
  lkpValue: string;
}

export interface StockTickler {
  lkpField: string;
  lkpValue: string;
  sortOrder: number;
}

export interface TicklerLicTech {
  licenseTechId: number;
  soeid: string;
  firstName: string;
  lastName: string;
  isActive: boolean;
  teamNum: string;
  licenseTechPhone: string;
  licenseTechFax: string;
  licenseTechEmail: string;
  techName: string;
}