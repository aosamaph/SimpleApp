import {Component, input, output} from '@angular/core';
import {currency, product} from '../types/entities';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ProductsService} from '../products/products.service';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent {
  productImage = input.required<string>(); // URL of the product image
  productName = input.required<string>();   // Name of the product
  productPrice = input.required<number>();   // Price of the product
  productCurrency = input.required<number>(); // Currency of the product
  productDescription = input.required<string>(); // Currency of the product
  productId = input.required<number>();
  categoryId = input.required<number>();

  product!:product;

  editFire = output<void>();

  protected readonly currency = currency;
  showModal = false;


  constructor(private productsService:ProductsService) {
  }

  ngOnInit():void{
    this.product = {
      id: this.productId(),
      name: this.productName(),
      description: this.productDescription(),
      price: this.productPrice(),
      currency: this.productCurrency(),
      image: this.productImage(),
      categoryId: this.categoryId()
    }
  }

  openEditProductPopup(): void {
    this.showModal = true; // Show the modal
  }

  closePopup(): void {
    this.showModal = false; // Hide the modal
  }

  onEditProduct(): void {
    this.productsService.editProduct(this.product).subscribe(() => {
      this.editFire.emit();
      this.closePopup();
    });
  }
}
