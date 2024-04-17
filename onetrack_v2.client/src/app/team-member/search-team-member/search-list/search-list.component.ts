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
  subscription: Subscription = new Subscription();

  constructor(
    private emplyService: EmployeeDataService,
    public modalService: ModalService
  ) {}

  ngOnInit() {
    this.subscription =
      this.emplyService.employeeSearchResultsChanged.subscribe(
        (employeeSearchResults: EmployeeSearchResult[]) => {
          this.searchEmployeeResults = employeeSearchResults;
        }
      );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
