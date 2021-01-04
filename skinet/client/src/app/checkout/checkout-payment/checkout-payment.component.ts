import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {
@Input() checkoutForm: FormGroup;
@ViewChild('cardNumber', {static: true}) cardNumberElement: ElementRef;
@ViewChild('cardExpiry', {static: true}) cardExpiryElement: ElementRef;
@ViewChild('cardCvc', {static: true}) cardCVCElement: ElementRef;
stripe: any;
cardNumber: any;
cardExpiry: any;
cardCvc: any;
cardError: any;
cardHandler = this.onChange.bind(this);

  constructor(private basketService: BasketService,
              private checkoutService: CheckoutService,
              private toastr: ToastrService,
              private router: Router) { }

  // tslint:disable-next-line:typedef
  ngAfterViewInit(){
    this.stripe = Stripe('pk_test_51I3MDxKXXWcn8gc4TahHp4X8OrH8AtyDNTpChvhFSPwmr8aXWaI9mmhdbZB2vktKLsppWND6h4Ur6TcKQdQBQQxe00nKxkXI9S');
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler);


    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCVCElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler);

  }

  // tslint:disable-next-line:typedef
  ngOnDestroy(){
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  // 266.
  // tslint:disable-next-line:typedef
  onChange({error}){
    if (error)
    {
      this.cardError = error.message;
    }
    else
    {
      this.cardError = null;
    }
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
