import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularToastifyModule, ToastService } from 'angular-toastify';

@Component({
  selector: 'app-update-page',
  standalone: true,
  imports: [ReactiveFormsModule, AngularToastifyModule],
  templateUrl: './update-page.component.html',
  styleUrl: './update-page.component.css'
})
export class UpdatePageComponent implements OnInit, OnDestroy {

  private productId?: string;
  private product?: Product;
  private productCheckInterval: any;

  productForm = new FormGroup({
    id: new FormControl(''),
    imageUrl: new FormControl(''),
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
    price: new FormControl('', [Validators.required, Validators.min(0), Validators.pattern(/^\d+(\.\d{1,2})?$/)]),
    discount: new FormControl('', [Validators.required, Validators.min(0), Validators.max(80), Validators.pattern(/^\d+$/)])
  });

  constructor(private toastService: ToastService, private productService: ProductService, private router: Router, private route: ActivatedRoute) { }

  getProduct(id: string): void {
    this.productService.getProductByID(id).subscribe({
      next: (response) => {
        this.product = response

        this.productForm.patchValue({
          ...this.product,
          price: this.product.price.toString(),
          discount: this.product.discount.toString()
        });
      },
      error: () => {
        this.toastService.error("This product does not exist in the API!");
        this.router.navigate([""])
      }
    })
  }

  checkProduct(id: string): void {
    this.productService.getProductByID(id).subscribe({
      error: () => {
        this.toastService.error("This product does not exist in the API!");
        this.router.navigate([""])
      }
    })
  }


  convertToProduct(): Product {
    let product: Product = {
      id: this.productId ?? "",
      imageUrl: this.productForm.get('imageUrl')?.value ?? "",
      name: this.productForm.get('name')?.value ?? "",
      description: this.productForm.get('description')?.value ?? "",
      price: parseFloat(this.productForm.get('price')?.value ?? "0"),
      discount: parseFloat(this.productForm.get('discount')?.value ?? "0")
    };
    return product;
  }

  resetProduct(): void {
    this.productForm.reset();
    this.productForm.patchValue({ id: this.product?.id })
  }

  updateProduct(): void {
    let product = this.convertToProduct()

    this.productService.updateProduct(product.id, product)
      .subscribe({
        next: () => {
          this.toastService.success("Product updated sucessfully!")
        },
        error: () => {
          this.toastService.error(`Values provided wasn't accepted by the API!`)
        }
      })
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.productId = JSON.parse(params.get('id') || '{}') as string;
      this.productCheckInterval = setInterval(() => {
        this.checkProduct(this.productId!);
      }, 10000);
      this.getProduct(this.productId);
    });
  }

  ngOnDestroy(): void {
    if (this.productCheckInterval) {
      clearInterval(this.productCheckInterval)
    }
  }

}
