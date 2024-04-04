import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  ModalService,
} from '../../_services';
import { Product, ProductItem } from '../../_Models';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.css',
})
@Injectable()
export class ProductEditComponent implements OnInit {
  loading: boolean = false;
  products: Product[] = [];

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.adminDataService.fetchProducts().subscribe((response) => {
      this.products = response;
      this.loading = false;
    });
  }
}
