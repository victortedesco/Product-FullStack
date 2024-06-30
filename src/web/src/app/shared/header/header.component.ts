import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  providers: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

export class HeaderComponent implements OnInit, OnDestroy{

  @ViewChild('status', { static: true }) statusElementRef!: ElementRef;
  private statusInterval: any;
  statusElement!: HTMLElement;

  constructor (private productService: ProductService) {}

  setStatus() : void {
    this.productService.getStatus()
    .subscribe({
      next: () => {
        this.statusElement.style.color = "green"
        this.statusElement.textContent = "● Online"
      },
      error: () => {
        this.statusElement.style.color = "red"
        this.statusElement.textContent = "● Offline"
      }
    });
  }

  ngOnInit(): void {
    this.statusElement = this.statusElementRef.nativeElement;

    this.statusInterval = setInterval(() => {
      this.setStatus()
    }, 10000)

    this.setStatus();
  }

  ngOnDestroy(): void {
    if (this.statusInterval) {
      clearInterval(this.statusInterval);
    }
  }
}

