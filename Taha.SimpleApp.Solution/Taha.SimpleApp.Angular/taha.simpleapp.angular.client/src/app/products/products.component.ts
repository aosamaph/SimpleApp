import { Component } from '@angular/core';
import {ProductCardComponent} from '../product-card/product-card.component';
import {currency, product} from '../types/entities';
import {ProductsService} from './products.service';
import {FormsModule} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    ProductCardComponent,
    FormsModule
  ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  products: product[] = [];
  showModal = false;
  catId!: number;

  // Temporary new product details
  newProduct:product = {
    name: '',
    price: 0,
    currency: 0,
    image: '',
    description: '',
    categoryId: 0
  };

  constructor(private route: ActivatedRoute, private productsService: ProductsService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.catId = Number.parseInt(params.get('id')!);
      this.fetchProducts(this.catId);
    });
  }

  fetchProducts(catId: number): void {
    this.productsService.getProducts(catId).subscribe(data => {
      this.products = data;
    });
  }

  openAddProductPopup(): void {
    this.showModal = true; // Show the modal
  }

  closePopup(): void {
    this.showModal = false; // Hide the modal
    this.resetNewProduct(); // Reset the form
  }

  resetNewProduct(): void {
    this.newProduct = {
      name: '',
      price: 0,
      currency: 0,
      image: '',
      description: '',
      categoryId: this.catId
    };
  }

  onAddProduct(): void {
    this.newProduct.categoryId = this.catId;
    this.productsService.addProduct(this.newProduct).subscribe(() => {
      this.fetchProducts(this.catId);
      this.closePopup();
    });
  }

  protected readonly currency = currency;
}
