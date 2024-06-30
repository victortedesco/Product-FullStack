import { Product } from './../models/product.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ProductService {
  private url: string = "https://localhost:7226/api/product";

  constructor(private http: HttpClient) { }

  getStatus() : Observable<string> {
    return this.http.get<string>(this.url + "/status", { responseType: 'text' as 'json'});
  }

  getProducts() : Observable<Product[]> {
    return this.http.get<Product[]>(this.url);
  }

  getProductByID(id: string) : Observable<Product> {
    return this.http.get<Product>(this.url + `/id/${id}`);
  }

  getProductsByName(name: string) : Observable<Product[]> {
    return this.http.get<Product[]>(this.url + `/name/${name}`);
  }

  addProduct(product: Product) : Observable<void> {
    return this.http.post<void>(`${this.url}`, product);
  }

  updateProduct(id: string, product: Product) : Observable<void> {
    console.log(product)
    return this.http.put<void>(`${this.url}/${id}`, product);
  }

  deleteProduct(id: string): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
