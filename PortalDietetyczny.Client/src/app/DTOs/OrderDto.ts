export interface OrderDto
{
  customerEmail: string,
  productsIds: number[],
  invoiceDto? : InvoiceDto
}

export interface InvoiceDto
{
  name : string,
  lastName :string,
  street : string,
  city : string
}
