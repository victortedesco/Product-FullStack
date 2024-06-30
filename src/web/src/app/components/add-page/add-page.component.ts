import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product.model';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastService } from 'angular-toastify';

@Component({
  selector: 'app-add-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-page.component.html',
  styleUrl: './add-page.component.css'
})
export class AddPageComponent {

  productForm = new FormGroup({
    imageUrl: new FormControl(''),
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
    price: new FormControl('', [Validators.required, Validators.min(0), Validators.pattern(/^\d+(\.\d{1,2})?$/)]),
    discount: new FormControl('', [Validators.required, Validators.min(0), Validators.max(80), Validators.pattern(/^\d+$/)])
  });

  constructor(private toastService: ToastService, private productService: ProductService) {}

  convertToProduct(): Product {
    let product: Product = {
      id: "",
      imageUrl: this.productForm.get('imageUrl')?.value ?? "",
      name: this.productForm.get('name')?.value ?? "",
      description: this.productForm.get('description')?.value ?? "",
      price: parseFloat(this.productForm.get('price')?.value ?? "0"),
      discount: parseFloat(this.productForm.get('discount')?.value ?? "0")
    };
    return product;
  }

  addProduct() : void {
    let product = this.convertToProduct()

    this.productService.addProduct(product)
    .subscribe({
      next: () => {
        this.toastService.success("Product added sucessfully!");
      },
      error: () => {
        this.toastService.error(`You provided values that will not be accepted by the API!`)
      }
    })
  }
}
