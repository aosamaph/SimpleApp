import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {product} from '../types/entities';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private apiUrl = 'https://localhost:7293/api/product';

  constructor(private http: HttpClient) {}

  getProducts(catId: number): Observable<product[]> {
    return this.http.get<product[]>(this.apiUrl + `/category/${catId}`);
  }

  addProduct(product: product): Observable<any> {
    return this.http.post(this.apiUrl, product);
  }

  editProduct(product: product) {
    return this.http.put(this.apiUrl, product);
  }
}
