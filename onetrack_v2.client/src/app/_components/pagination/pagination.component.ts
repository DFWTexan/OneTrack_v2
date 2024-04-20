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
  pageRange: any[] = [];

  constructor(public paginationComService: PaginationComService) {}

  ngOnChanges() {
    this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
    this.calculatePageRange();
  }

  calculatePageRange() {
    let start = Math.max(this.currentPage - 2, 1);
    let end = Math.min(start + 4, this.totalPages);
    start = Math.max(end - 4, 1);
    this.pageRange = Array(end - start + 1).fill(0).map((_, i) => start + i);
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
