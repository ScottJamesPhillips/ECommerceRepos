import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {
@Input() checkoutForm: FormGroup;

  constructor(private basketService: BasketService,
              private checkoutService: CheckoutService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  // 246
  // tslint:disable-next-line:typedef
  getOrderToCreate(basket: IBasket){
    return{
      basketId: basket.id,
      // tslint:disable-next-line:radix
      deliveryMethodId: parseInt(this.checkoutForm.get('deliveryForm').get('deliveryMethod').value),
      shipToAddress: this.checkoutForm.get('addressForm').value
    };
  }

  // 246
  // tslint:disable-next-line:typedef
  submitOrder(){
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutService.createOrder(orderToCreate).subscribe((order: IOrder) => {
       this.toastr.success('Order created succesfully');
       this.basketService.deleteLocalBasket(basket.id);
      //  console.log(order);
       const navigationExtras: NavigationExtras = {state: order};
       this.router.navigate(['checkout/Success'], navigationExtras);
     }, error => {
       this.toastr.error(error);
       console.log(error);
     });
  }



}
