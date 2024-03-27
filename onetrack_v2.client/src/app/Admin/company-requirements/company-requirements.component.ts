import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-company-requirements',
  templateUrl: './company-requirements.component.html',
  styleUrl: './company-requirements.component.css',
})
@Injectable()
export class CompanyRequirementsComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  states: any[] = [];
  selectedWorkState: string = 'Select';
  selectedResState: string = 'Select';

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.states = ['Select', ...this.conService.getStates()];
  }

  changeWorkState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedWorkState = value;
    
    if (value === 'Select') {
      return;
    } 
    // else {
    //   this.adminDataService.fetchCompanyRequirements(value).subscribe((response) => {
    //     this.loading = false;
    //   });
    // }
  }

  changeResidentState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedResState = value;
  }

  ngOnDestroy(): void {}
}
