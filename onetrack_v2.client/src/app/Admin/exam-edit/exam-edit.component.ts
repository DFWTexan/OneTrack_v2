import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ModalService,
} from '../../_services';
import { Exam } from '../../_Models';

@Component({
  selector: 'app-exam-edit',
  templateUrl: './exam-edit.component.html',
  styleUrl: './exam-edit.component.css',
})
@Injectable()
export class ExamEditComponent implements OnInit {
  loading: boolean = false;
  stateProvinces: any[] = [];
  selectedStateProvince: string = 'Select';
  examItems: Exam[] = [];

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['Select', ...this.conService.getStateProvinces()];
  }

  changeStateProvince(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    if (value === 'Select') {
      // this.adminDataService.fetchCities(value).subscribe((response) => {
      //   this.adminDataService.cities = response;
      //   this.adminDataService.citiesChanged.next(this.adminDataService.cities);
      // });
    } else {
      // this.adminDataService.fetchCities(value).subscribe((response) => {
      //   this.adminDataService.cities = response;
      //   this.adminDataService.citiesChanged.next(this.adminDataService.cities);
      // });
    }
  }
}
