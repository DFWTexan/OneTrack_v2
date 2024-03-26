export interface Company {
    companyId: number;
    companyAbv: string;
    companyType: string;
    companyName: string;
    tin: string | null;
    naicnumber: string | null;
    addressId: number;
    address1: string | null;
    address2: string | null;
    city: string | null;
    state: string;
    phone: string | null;
    country: string;
    zip: string | null;
    fax: string | null;
  }