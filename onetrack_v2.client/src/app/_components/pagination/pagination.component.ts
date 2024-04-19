import { Component, EventEmitter, Injectable, Input, Output } from '@angular/core';

import { PaginationComService } from '../../_services';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css'
})
@Injectable()
export class PaginationComponent {
  @Input() currentPage: number = 1;
  @Input() totalItems: number = 0;
  @Input() itemsPerPage: number = 25;
  @Output() pageChange = new EventEmitter<number>();

  totalPages: number = 0;

  constructor(public paginationComService: PaginationComService) {}

  ngOnChanges() {
    this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.pageChange.emit(this.currentPage - 1);
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.pageChange.emit(this.currentPage + 1);
    }
  }

  goToPage(page: number) {
    this.pageChange.emit(page);
  }
}
