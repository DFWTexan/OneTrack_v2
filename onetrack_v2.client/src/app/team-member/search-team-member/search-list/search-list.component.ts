import { Component, OnInit, OnDestroy } from '@angular/core';

import { EmployeeSearchResult } from '../../../_Models';
import { Subscription } from 'rxjs';

import { EmployeeDataService, ModalService } from '../../../_services';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrl: './search-list.component.css',
})
export class SearchListComponent implements OnInit, OnDestroy {
  searchEmployeeResults: EmployeeSearchResult[] = [];
  paginatedResults: EmployeeSearchResult[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 50; // Adjust as needed
  subscription: Subscription = new Subscription();
  Math = Math;

  constructor(
    private emplyService: EmployeeDataService,
    public modalService: ModalService
  ) {}

  ngOnInit() {
    this.subscription =
      this.emplyService.employeeSearchResultsChanged.subscribe(
        (employeeSearchResults: EmployeeSearchResult[]) => {
          this.searchEmployeeResults = employeeSearchResults;
          this.updatePaginatedResults();
        }
      );
  }

  updatePaginatedResults() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedResults = this.searchEmployeeResults.slice(startIndex, endIndex);
  }

  goToPage(n: number): void {
    this.currentPage = n;
    this.updatePaginatedResults();
  }

  nextPage(): void {
    this.goToPage(this.currentPage + 1);
  }

  previousPage(): void {
    this.goToPage(this.currentPage - 1);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
