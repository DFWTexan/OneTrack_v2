import { Component, OnInit, OnDestroy, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { EmployeeSearchResult } from '../../../_Models';
import { Subscription } from 'rxjs';

import { EmployeeDataService, ModalService } from '../../../_services';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.css'],
})
export class SearchListComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource<EmployeeSearchResult>([]);
  searchEmployeeResults: EmployeeSearchResult[] = [];

  displayedColumns: string[] = ['employeeID', 'geid', 'name', 'resStateAbv', 'workStateAbv', 'branchName', 'scoreNumber', 'employmentID', 'actions'];

  subscription: Subscription = new Subscription();

  constructor(
    private emplyService: EmployeeDataService,
    public modalService: ModalService
  ) {}

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit() {
    this.subscription =
      this.emplyService.employeeSearchResultsChanged.subscribe(
        (employeeSearchResults: EmployeeSearchResult[]) => {
          this.searchEmployeeResults = employeeSearchResults;
          this.dataSource.data = employeeSearchResults;
        }
      );
  }

  action1(member: any) {
    // this.modalService.openModal('Action 1');
  }

  action2(member: any) {
    // this.modalService.openModal('Action 2');
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}