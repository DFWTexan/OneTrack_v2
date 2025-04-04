import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  EmployeeDataService,
  ErrorMessageService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../_services';
import { EmployeeSearchResult } from '../../../_Models';

@Component({
  selector: 'app-add-indexer',
  templateUrl: './add-indexer.component.html',
  styleUrl: './add-indexer.component.css',
})
export class AddIndexerComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  indexForm: FormGroup;
  states: string[] = ['Select', ...this.conService.getStates()];
  branchNames: { value: any; label: string }[] = [];
  scoreNumbers: { value: number; label: string }[] = [];
  documentTypes: string[] = [];
  defaultDocumentType: string = 'Select';
  documentSubTypes: string[] = [];
  selectedDocumentType: any | null = null;
  FileDisplayMode = 'CHOOSEFILE'; //--> CHOSEFILE / ATTACHMENT
  file: File | null = null;
  fileUri: string | null = null;
  document: string = '';
  employee: EmployeeSearchResult | null = null;

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private employeeDataService: EmployeeDataService,
    public miscDataService: MiscDataService,
    public appComService: AppComService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder,
    private userInfoDataService: UserAcctInfoDataService
  ) {
    this.indexForm = this.fb.group({});
  }

  ngOnInit() {
    this.employee = this.employeeDataService.selectedEmployee;
    this.subscriptionData.add(
      this.employeeDataService.selectedEmployeeChanged.subscribe(
        (employee: EmployeeSearchResult | null) => {
          if (employee !== null) {
            this.employee = employee;
          }
        }
      )
    );

    this.branchNames = [
      { value: null, label: 'Select' },
      ...this.dropdownDataService.branchNames,
    ];

    this.scoreNumbers = [
      { value: 0, label: 'Select' },
      ...this.dropdownDataService.scoreNumbers,
    ];

    this.documentTypes = ['Select', ...this.dropdownDataService.documentTypes];
  }

  onDocumentTypeChange(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedDocumentType = value === 'Select' ? null : value;
    this.getDocumentSubTypes();
  }

  private getDocumentSubTypes() {
    this.subscriptionData.add(
      this.miscDataService
        .fetchDocumetSubTypes(this.selectedDocumentType)
        .subscribe((documentSubTypes) => {
          this.documentSubTypes = documentSubTypes;
        })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;
  
    if (this.indexForm.valid) {
      const formData = {
        employeeID: this.employee?.employeeID || 0,
        firstName: this.employee?.firstName || '',
        lastName: this.employee?.lastName || '',
        geid: this.employee?.geid || '',
        workState: this.indexForm.get('workState')?.value || '',
        licenseState: this.indexForm.get('licenseState')?.value || '',
        branchName: this.indexForm.get('branchName')?.value || '',
        scoreNumber: this.indexForm.get('scoreNum')?.value || '',
        documentType: this.selectedDocumentType || '',
        documentSubType: this.indexForm.get('docSubType')?.value || '',
      };
  
      // this.errorMessageService.showLoadingMessage('Submitting data, please wait...');
      this.employeeDataService.updateEmployeeIndexer(formData).subscribe({
        next: (response) => {
          if (response.success && response.statusCode === 200) {
            // this.errorMessageService.showSuccessMessage('Data submitted successfully!');
            this.onCancel();
          } else {
            // this.errorMessageService.showErrorMessage(response.errMessage || 'An unknown error occurred.');
          }
        },
        error: (error) => {
          console.error('Error submitting data:', error);
          // this.errorMessageService.showErrorMessage('Error submitting data: ' + (error.message || 'Unknown error.'));
        },
        complete: () => {
          // this.errorMessageService.hideLoadingMessage();
        },
      });
    } else {
      // this.errorMessageService.showErrorMessage('Please fill all required fields.');
    }
  }

  onFileSelected(event: any) {
    // some code here
  }

  onCancel() {
    const modalDiv = document.getElementById('modal-document-index');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
