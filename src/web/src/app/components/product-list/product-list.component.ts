import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ToastService } from 'angular-toastify';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit, OnDestroy {
  public products: Product[] = [];
  private refreshInterval: any;

  @ViewChild('searchInput', { static: true }) searchInputElementRef!: ElementRef;
  searchInputElement!: HTMLInputElement;

  constructor(private toastService: ToastService, private productService: ProductService, private router: Router) { }

  isEmpty = (text: string): boolean => {
    return text === null || text.match(/^ *$/) !== null;
  };

  searchProduct = (event: KeyboardEvent): void => {
    const element = event.currentTarget as HTMLInputElement
    const value = element.value

    if (event.key !== 'Enter') return;

    if (this.isEmpty(value)) {
      this.productService.getProducts()
        .subscribe({
          next: (response) => {
            this.products = response
          }
        });
      return;
    }
    this.productService.getProductsByName(value)
      .subscribe((response) => {
        this.products = response
      })
  };

  getProducts(): void {
    this.productService.getProducts()
      .subscribe({
        next: (response) => {
          this.products = response;
        },
        error: () => {
          this.products = [];
        }
      });
  }

  editProduct(productId: string): void {
    this.router.navigate(['/update', { id: JSON.stringify(productId) }]);
  }

  deleteProduct(productId: string): void {

    this.productService.deleteProduct(productId).subscribe(() => {
      this.toastService.info("Product deleted!");
      this.products = this.products.filter(arrProduct => productId !== arrProduct.id!)
    });
  }

  ngOnInit(): void {
    this.searchInputElement = this.searchInputElementRef.nativeElement;

    this.refreshInterval = setInterval(() => {
      if (this.isEmpty(this.searchInputElement.value)) {
        this.getProducts()
      }
    }, 10000);

    this.getProducts();
  }

  ngOnDestroy(): void {
    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
    }
  }
}
