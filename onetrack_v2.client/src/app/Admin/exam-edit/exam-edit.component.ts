import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  MiscDataService,
  ModalService,
} from '../../_services';
import { Exam } from '../../_Models';

@Component({
  selector: 'app-exam-edit',
  templateUrl: './exam-edit.component.html',
  styleUrl: './exam-edit.component.css',
})
@Injectable()
export class ExamEditComponent implements OnInit, OnDestroy{
  isLoading: boolean = false;
  stateProvinces: any[] = [];
  examDeliveryMethods:  any[] = [];
  examProviders: {value: number, label: string}[] = [];
  selectedStateProvince: string = 'Select';
  examItems: Exam[] = [];

  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public miscDataService: MiscDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['Select', ...this.conService.getStateProvinces()];
    this.fetchEditExamDropdowns();
  }

  private fetchEditExamDropdowns(): void {
    this.subscriptionData.add(
      this.adminDataService.fetchDropdownListItems('ExamDelivery').subscribe((response) => {
        this.examDeliveryMethods = ['Select Method', ...response.map((item: any) => item.lkpValue)];
      })
    );
    this.subscriptionData.add(
      this.miscDataService.fetchExamProviders().subscribe((response) => {
        this.examProviders = [ {value: 0, label: 'Select Provider'} , ...response];
      }) 
    );
  }

  changeStateProvince(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedStateProvince = value;

    if (value === 'Select') {
      // this.adminDataService.fetchCities(value).subscribe((response) => {
      //   this.adminDataService.cities = response;
      //   this.adminDataService.citiesChanged.next(this.adminDataService.cities);
      // });
    } else {
      this.adminDataService.fetchExamItems(value).subscribe((response) => {
        this.examItems = response;
      });
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
