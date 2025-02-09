import { Component, OnInit, OnDestroy } from '@angular/core';

import { EmployeeSearchResult } from '../../../_Models';
import { Subscription } from 'rxjs';

import {
  AppComService,
  EmployeeDataService,
  ModalService,
  PaginationComService,
} from '../../../_services';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrl: './search-list.component.css',
})
export class SearchListComponent implements OnInit, OnDestroy {
  searchEmployeeResults: EmployeeSearchResult[] = [];
  subscription: Subscription = new Subscription();

  constructor(
    public emplyService: EmployeeDataService,
    public modalService: ModalService,
    public appComService: AppComService,
    public paginationComService: PaginationComService
  ) {}

  ngOnInit() {
    this.subscription.add(
      this.emplyService.employeeSearchResultsChanged.subscribe(
        (employeeSearchResults: EmployeeSearchResult[]) => {
          this.searchEmployeeResults = employeeSearchResults;
          this.paginationComService.updateDisplayItems(
            this.searchEmployeeResults
          );
          this.paginationComService.updatePaginatedResults();
        }
      )
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
