import { Routes } from '@angular/router';
import {ProductsComponent} from './products/products.component';

export const routes: Routes = [
  { path: 'product/category/:id', component: ProductsComponent },
  { path: '**', redirectTo: '/product/category/1' }
];
