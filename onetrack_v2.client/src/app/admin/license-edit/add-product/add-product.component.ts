import { Component } from '@angular/core';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css'
})
export class AddProductComponent {
  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-product-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
