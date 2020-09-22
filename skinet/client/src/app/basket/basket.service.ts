import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/models/product';
import {Basket} from '../shared/models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }
    // tslint:disable-next-line:typedef
    getBasket(id: string)
    {
      return this.http.get(this.baseUrl + 'basket?id=' + id).pipe(map((basket: IBasket) => {
        this.basketSource.next(basket);
        this.calculatesTotals();
      }));
    }

    // tslint:disable-next-line:typedef
    setBasket(basket: IBasket)
    {
      return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
        this.basketSource.next(response);
        this.calculatesTotals();
      }, error => {
        console.log(error);
      });
    }

    // tslint:disable-next-line:typedef
    getCurrentBasketValue()
    {
      return this.basketSource.value;
    }

    // tslint:disable-next-line:typedef
    addItemToBasket(item: IProduct, quantity = 1)
    {
      const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
      let basket = this.getCurrentBasketValue();
      if (basket === null)
      {
        basket = this.createBasket();
      }
      basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
      this.setBasket(basket);
    }

    // tslint:disable-next-line:typedef
    decrementItemQuantity(item: IBasketItem) {
      const basket = this.getCurrentBasketValue();
      const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
      if (basket.items[foundItemIndex].quantity > 1){
        basket.items[foundItemIndex].quantity--;
        this.setBasket(basket);
      }
      else {
        this.removeItemFromBasket(item);
      }
    }

  // tslint:disable-next-line:typedef
  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if (basket.items.some(x => x.id === item.id))
    {
      basket.items = basket.items.filter(i => i.id !== item.id);
      if (basket.items.length > 0)
      {
        this.setBasket(basket);
      }
      else
      {
        this.deleteBasket(basket);
      }
    }
  }

  // tslint:disable-next-line:typedef
  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }

    // tslint:disable-next-line:typedef
    incrementItemQuantity(item: IBasketItem) {
      const basket = this.getCurrentBasketValue();
      const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
      basket.items[foundItemIndex].quantity++;
      this.setBasket(basket);
    }

    private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
      // check for item id, if already exists in basket then know only want to increase quantity rather than add productId to basket...
      const index = items.findIndex(i => i.id === itemToAdd.id);
      if (index === -1)
      {
        itemToAdd.quantity = quantity;
        items.push(itemToAdd);
      }
      else
      {
        items[index].quantity += quantity;
      }
      return items;
    }

  createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

    private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
      return{
        id: item.id,
        productName: item.name,
        price: item.price,
        pictureUrl: item.pictureUrl,
        quantity,
        brand: item.productBrand,
        type: item.productType
      };
    }

    // tslint:disable-next-line:typedef
    private calculatesTotals(){
      const basket = this.getCurrentBasketValue();
      const shipping = 0;
      const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
      const total = subtotal + shipping;
      console.log('Sub-Total:' + subtotal);
      console.log('Total:' + total);
      this.basketTotalSource.next({shipping, total, subtotal});
    }
  }
