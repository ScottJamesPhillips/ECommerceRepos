import {v4 as uuidv4} from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
  client?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice?: number;
}

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export class Basket implements IBasket {
    // exporting instance of class Basket using IBasket interface, 'guid' gives unique ID number
    id = uuidv4();
    items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
