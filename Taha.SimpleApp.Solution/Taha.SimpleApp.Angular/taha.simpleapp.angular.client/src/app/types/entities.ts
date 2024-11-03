export type category ={
  id?:number,
  name:string
}

export type product = {
  id?: number,
  name: string,
  price: number,
  currency: currency,
  image: string,
  description: string,
  categoryId: number
}


export enum currency
{
  None = 0,
  USD = 1,
  EUR = 2,
  GBP = 3,
  EGP = 4,
}
