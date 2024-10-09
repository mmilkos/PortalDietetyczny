export class OrderSummaryDto {
  orderId: string;
  amount: number;
  customerEmail: string;
  orderStatus: string;
  dietsNames: string;
  hasInvoice: boolean;
  invoiceId: number | null;
}
