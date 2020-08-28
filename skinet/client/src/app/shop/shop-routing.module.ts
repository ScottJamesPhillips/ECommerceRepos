import { NgModule } from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ShopComponent} from '../shop/shop.component';
import {ProductDetailsComponent} from '../shop/product-details/product-details.component';

const routes: Routes = [
  // Path - shop component (localhost:4200/shop)
  {path: '', component: ShopComponent},
  // Path - product detail component (localhost:4200/shop/1)
  {path: ':id', component: ProductDetailsComponent,
  data: {breadcrumb: {alias: 'productDetails'}}},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class ShopRoutingModule { }
