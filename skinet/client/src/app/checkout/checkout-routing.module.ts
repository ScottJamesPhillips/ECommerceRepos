import { NgModule } from '@angular/core';
import { CheckoutComponent } from './checkout.component';
import {Routes, RouterModule} from '@angular/router';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';

const routes: Routes = [
  {path: '', component: CheckoutComponent},
  {path: 'Success', component: CheckoutSuccessComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CheckoutRoutingModule { }