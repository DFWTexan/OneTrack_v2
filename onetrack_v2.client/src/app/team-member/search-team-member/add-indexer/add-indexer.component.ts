import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../_services';

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

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public miscDataService: MiscDataService,
    public appComService: AppComService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder,
    private userInfoDataService: UserAcctInfoDataService
  ) {
    this.indexForm = this.fb.group({});
  }

  ngOnInit() {
    this.branchNames = [{ value: null, label: 'Select' }, ...this.dropdownDataService.branchNames];
    
    this.scoreNumbers = [{ value: 0, label: 'Select' }, ...this.dropdownDataService.scoreNumbers];
    
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
    // some code here
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
