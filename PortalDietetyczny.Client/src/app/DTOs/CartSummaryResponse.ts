export interface CartSummaryResponse
{
  products: number[]
}

export interface CartProduct
{
  id: number,
  name: string,
  price: number
  photoUrl: string
}


export interface CartSummaryRequest
{
  productsIds : number[]
}
