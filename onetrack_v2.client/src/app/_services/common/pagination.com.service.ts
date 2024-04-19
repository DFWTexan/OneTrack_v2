import { Injectable } from '@angular/core';
import { Subject, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PaginationComService {
  displayItems: any[] = [];
  displayItemsChanged = new Subject<any[]>();
  paginatedResults: any[] = [];
  paginatedResultsChanged = new Subject<any[]>();
  currentPage: number = 1;
  currentPageChanged = new Subject<number>();
  itemsPerPage: number = 50; // Adjust as needed
  itemsPerPageChanged = new Subject<number>();

  subscription: Subscription = new Subscription();
  Math = Math;

  constructor() {}

  updateDisplayItems(items: any[]) {
    this.displayItems = items;
    this.displayItemsChanged.next(this.displayItems);
  }

  updatePaginatedResults() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedResults = this.displayItems.slice(startIndex, endIndex);
    // this.paginatedResults = items;
    this.paginatedResultsChanged.next(this.paginatedResults);
  }

  changeCurrentPage(page: number) {
    this.currentPage = page;
    this.currentPageChanged.next(page);
  }

  changeItemsPerPage(itemsPerPage: number) {
    this.itemsPerPage = itemsPerPage;
    this.itemsPerPageChanged.next(itemsPerPage);
  }
}
