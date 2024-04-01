import { Component, Injectable, OnInit } from '@angular/core';
import {
  AdminComService,
  AdminDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-dropdown-list-edit',
  templateUrl: './dropdown-list-edit.component.html',
  styleUrl: './dropdown-list-edit.component.css',
})
@Injectable()
export class DropdownListEditComponent implements OnInit {
  loading: boolean = false;
  dropdownListTypes: any[] = [];  
  dropdownListItems: any[] = [];
  selectedDropdownListType: string = 'AgentStatus';

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.adminDataService.fetchDropdownListTypes().subscribe((response) => {
      this.dropdownListTypes = response;
      this.loading = false;
    });
  }
  
  changeDropdownListType(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    // this.selectedCompanyType = value;

    // if (value === 'Select Company Type') {
    //   this.companies = [];
    //   this.loading = false;
    //   return;
    // } else {
    //   this.adminDataService.fetchCompanies(value).subscribe((response) => {
    //     this.companies = response;
    //     this.loading = false;
    //   });
    // }
  }
}
