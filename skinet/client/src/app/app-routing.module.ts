import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/test-error/server-error/server-error.component';
import { NotFoundComponent } from './core/test-error/not-found/not-found.component';


const routes: Routes = [
  // Path - home component (localhost:4200/)
  {path: '', component: HomeComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  // Path - shop component (localhost:4200/shop)
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},
  // Path - redirect to home if URL incorrect
  {path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
