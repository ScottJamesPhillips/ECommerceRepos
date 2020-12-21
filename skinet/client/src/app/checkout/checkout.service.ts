import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { IDeliveryMethod } from '../shared/models/deliveryMethods';
import { map } from 'rxjs/operators';
import { IOrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line:typedef
  getDeliveryMethods(){
    return this.http.get(this.baseUrl + 'orders/deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) => {
        return dm.sort((a, b) => b.price - a.price);
      })
    );
  }

  // 246
  // tslint:disable-next-line:typedef
  createOrder(order: IOrderToCreate){
    try
    {
      return this.http.post(this.baseUrl + 'orders', order);
    }
    catch (error)
    {
      console.log(error);
    }
  }
}
