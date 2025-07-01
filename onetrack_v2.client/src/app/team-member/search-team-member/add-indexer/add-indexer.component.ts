import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  EmailDataService,
  EmployeeDataService,
  ErrorMessageService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../_services';
import { EmployeeSearchResult } from '../../../_Models';
import { FileUploadComponent } from '../../../_components/file-upload/file-upload.component';

@Component({
  selector: 'app-add-indexer',
  templateUrl: './add-indexer.component.html',
  styleUrl: './add-indexer.component.css',
})
export class AddIndexerComponent implements OnInit, OnDestroy {
  @ViewChild('fileUpload') fileUploadComponent!: FileUploadComponent;
  isFormSubmitted: boolean = false;
  savedMessage: string | null = null;
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
  files: File[] = [];
  fullFilePathUri: string | null = null;
  isDocumentUploaded: boolean = false;
  uploadType: string = 'AttachmentLoc';

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private employeeDataService: EmployeeDataService,
    public miscDataService: MiscDataService,
    public appComService: AppComService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder,
    private userInfoDataService: UserAcctInfoDataService,
    private emailDataService: EmailDataService,
    private cdr: ChangeDetectorRef,
  ) {
    this.indexForm = this.fb.group({
      workState: ['Select'],
      licenseState: ['Select'],
      branchName: [null],
      scoreNum: [0],
      docSubType: [null],
    });
  }

  ngOnInit() {
    this.employee = this.employeeDataService.selectedEmployee;
    this.subscriptionData.add(
      this.employeeDataService.selectedEmployeeChanged.subscribe(
        (employee: EmployeeSearchResult | null) => {
          if (employee !== null) {
            this.employee = employee;
            this.cdr.detectChanges();
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

    this.indexForm.reset({
      workState: 'Select',
      licenseState: 'Select',
      branchName: null,
      scoreNum: 0,
      docSubType: null,
    });
  }

  fileUploadCompleted(filePath: string) {
    this.fullFilePathUri = filePath;
    // this.companyReqForm.patchValue({
    //   document: filePath,
    // });
    // this.companyReqForm.markAsDirty();
    this.files.push(
      new File([filePath], 'uploaded-file', { type: 'application/pdf' })
    );
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

  onFilesChanged(files: File[]) {
    this.files = files;
  }

  onSubmit() {
    this.isFormSubmitted = true;

    if (this.indexForm.valid) {
      const formData = new FormData();

      // Append form fields
      formData.append(
        'EmployeeID',
        this.employee?.employeeID?.toString() || '0'
      );
      formData.append('FirstName', this.employee?.firstName || '');
      formData.append('LastName', this.employee?.lastName || '');
      formData.append('Geid', this.employee?.geid || '');
      formData.append(
        'WorkState',
        this.indexForm.get('workState')?.value || ''
      );

      formData.append(
        'LicenseState',
        this.indexForm.get('licenseState')?.value || ''
      );

      formData.append(
        'BranchName',
        this.indexForm.get('branchName')?.value || ''
      );
      formData.append(
        'ScoreNumber',
        this.indexForm.get('scoreNum')?.value || ''
      );
      formData.append('DocumentType', this.selectedDocumentType || '');
      formData.append(
        'DocumentSubType',
        this.indexForm.get('docSubType')?.value || ''
      );

      // Append files
      if (this.files && this.files.length > 0) {
        this.files.forEach((file) => {
          formData.append('Files', file, file.name);
        });
      }

      this.employeeDataService.updateEmployeeIndexer(formData).subscribe({
        next: (response) => {
          // Reset form submission state
          this.isFormSubmitted = false;
          
          // Reset the reactive form
          this.indexForm.reset({
            workState: 'Select',
            licenseState: 'Select',
            branchName: null,
            scoreNum: 0,
            docSubType: null,
          });

          // Reset component properties
          this.selectedDocumentType = 'Select';
          this.documentSubTypes = [];
          this.files = [];
          
          // Clear attached files in email service
          this.emailDataService.setAttachedFiles([]);
          
          // Reset file upload component if it exists
          if (this.fileUploadComponent) {
            // Reset file upload component - adjust method name as needed
            // this.fileUploadComponent.reset();
          }
          
          // Trigger change detection
          this.cdr.detectChanges();

          this.appComService.updateAppMessage(
            'Data submitted successfully.'
          );

          const modalDiv = document.getElementById('modal-document-index');
          if (modalDiv != null) {
            modalDiv.style.display = 'none';
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
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];

      this.files.push(file);

      // if (file) {
      //   this.file = file;
      //   this.fileUri = URL.createObjectURL(file);
      //   this.document = file.name;
      // }
    }
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
