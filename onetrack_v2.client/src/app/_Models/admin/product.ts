export interface Product {
  productId: number;
  productName: string;
  productAbv: string | null;
  effectiveDate: string | null;
  expireDate: string | null;
  isActive: boolean;
}
