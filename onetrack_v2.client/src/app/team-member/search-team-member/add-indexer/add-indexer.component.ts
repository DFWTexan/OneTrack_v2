import {
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
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
  uploadType: string = 'BuildLoc';

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
    private cdr: ChangeDetectorRef
  ) {
    this.indexForm = this.fb.group({
      workState: ['Select'],
      licenseState: ['Select'],
      branchName: [null],
      scoreNum: [0],
      docType: [null],
      docSubType: [null],
    });
  }

  ngOnInit() {
    this.subscriptionData.add(
      this.appComService.indexInfoResetToggleChanged.subscribe(
        (indexInfoResetToggle: boolean) => {
          console.log('🔴 indexInfoResetToggleChanged - clearing files');
          this.file = null;
          this.files = [];
          this.fileUri = null;
          this.fullFilePathUri = null;
          this.defaultDocumentType = 'Select';
          this.indexForm.reset({
            workState: 'Select',
            licenseState: 'Select',
            branchName: null,
            scoreNum: 0,
            docType: 'Select',
          });

          if (this.fileUploadComponent) {
            this.fileUploadComponent.resetComponent();
          }
        }
      )
    );

    this.employee = this.employeeDataService.selectedEmployee;
    this.subscriptionData.add(
      this.employeeDataService.selectedEmployeeChanged.subscribe(
        (employee: EmployeeSearchResult | null) => {
          if (employee !== null) {
            const previousEmployee = this.employee;
            this.employee = employee;

            // Only clear files and reset form if this is a different employee
            if (
              !previousEmployee ||
              previousEmployee.employeeID !== employee.employeeID
            ) {
              console.log(
                '🔴 selectedEmployeeChanged - different employee, clearing files and resetting form'
              );
              this.files = [];
              this.file = null;
              this.fileUri = null;
              this.fullFilePathUri = null;
              this.selectedDocumentType = 'Select';
              this.documentSubTypes = [];

              // Reset file upload component if it exists
              if (this.fileUploadComponent) {
                this.fileUploadComponent.resetComponent();
              }

              this.indexForm.reset({
                workState: 'Select',
                licenseState: 'Select',
                branchName: null,
                scoreNum: 0,
                docSubType: null,
              });
            } else {
              console.log(
                '🟡 selectedEmployeeChanged - same employee, keeping files and form state'
              );
            }
          } else {
            console.log(
              '🔴 selectedEmployeeChanged - employee is null, clearing everything'
            );
            this.employee = null;
            this.files = [];
            this.file = null;
            this.fileUri = null;
            this.fullFilePathUri = null;
            this.selectedDocumentType = 'Select';
            this.documentSubTypes = [];

            if (this.fileUploadComponent) {
              this.fileUploadComponent.resetComponent();
            }

            this.indexForm.reset({
              workState: 'Select',
              licenseState: 'Select',
              branchName: null,
              scoreNum: 0,
              docSubType: null,
            });
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
  // Add this method to track all file-related events
  trackFileEvent(eventName: string, data?: any) {
    console.log(`🔍 File Event: ${eventName}`, data);
  }

  // async fileUploadCompleted(filePath: string) {
  //   this.trackFileEvent('fileUploadCompleted', filePath);
  //   console.log('🔵 fileUploadCompleted called with:', filePath);
  //   this.fullFilePathUri = filePath;

  //   try {
  //     // Fetch the actual file content as a blob
  //     const response = await fetch(filePath);
  //     const blob = await response.blob();

  //     // Extract the file name from the path
  //     const fileName = filePath.split('\\').pop()?.split('/').pop() || 'file';

  //     // Determine the correct MIME type based on file extension
  //     const fileExtension = fileName.split('.').pop()?.toLowerCase();
  //     let mimeType = 'application/octet-stream'; // default

  //     switch (fileExtension) {
  //       case 'pdf':
  //         mimeType = 'application/pdf';
  //         break;
  //       case 'docx':
  //         mimeType =
  //           'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
  //         break;
  //       case 'doc':
  //         mimeType = 'application/msword';
  //         break;
  //       case 'txt':
  //         mimeType = 'text/plain';
  //         break;
  //       case 'jpg':
  //       case 'jpeg':
  //         mimeType = 'image/jpeg';
  //         break;
  //       case 'png':
  //         mimeType = 'image/png';
  //         break;
  //     }

  //     // Create a proper File object with the blob content
  //     const file = new File([blob], fileName, { type: mimeType });
  //     this.files.push(file);
  //     console.log(
  //       '✅ File added to array:',
  //       fileName,
  //       'Total files:',
  //       this.files.length
  //     );
  //   } catch (error) {
  //     console.error('❌ Error loading file:', error);
  //     // Optionally show error message to user
  //   }
  // }

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
    console.log('🔵 onFilesChanged called with:', files.length, 'files');
    this.files = [...files]; // Create a new array to avoid reference issues
    console.log('📝 Files array updated, new length:', this.files.length);

    // Log each file for debugging
    this.files.forEach((file, index) => {
      console.log(`File ${index + 1}:`, file.name, file.size, file.type);
    });
  }
  onSubmit() {
    this.isFormSubmitted = true;

    // Add debugging
    console.log('Files array length:', this.files.length);
    console.log('Files array contents:', this.files);
    console.log('Form valid:', this.indexForm.valid);

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
        console.log('Adding files to FormData...');
        this.files.forEach((file, index) => {
          console.log(`Adding file ${index}:`, file.name, file.size, file.type);
          formData.append('Files', file, file.name);
        });
      } else {
        console.log('No files to append - files array is empty or null');
      }

      // Debug FormData contents
      console.log('FormData contents:');
      for (let [key, value] of (formData as any).entries()) {
        console.log(key, value);
      }

      this.employeeDataService.updateEmployeeIndexer(formData).subscribe({
        next: (response) => {
          // Reset form submission state
          this.isFormSubmitted = false;
          this.appComService.updateIndexInfoResetToggle();

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
            this.fileUploadComponent.resetComponent();
          }

          // Trigger change detection
          this.cdr.detectChanges();

          this.appComService.updateAppMessage('Data submitted successfully.');

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
    this.trackFileEvent('onFileSelected', event);
    console.log('🔵 onFileSelected called with:', event);

    // If event is a File object directly
    if (event instanceof File) {
      this.files.push(event);
      console.log(
        '✅ File added via fileSelected:',
        event.name,
        'Total files:',
        this.files.length
      );
      return;
    }

    // If event is an array of files
    if (Array.isArray(event) && event.length > 0 && event[0] instanceof File) {
      event.forEach((file: File) => {
        this.files.push(file);
        console.log('✅ File added from array:', file.name);
      });
      console.log('✅ Total files after adding array:', this.files.length);
      return;
    }

    // If event is from input element (change event)
    if (event && event.target) {
      const target = event.target as HTMLInputElement;
      if (target.files && target.files.length) {
        for (let i = 0; i < target.files.length; i++) {
          const file = target.files[i];
          this.files.push(file);
          console.log('✅ File selected from input:', file.name);
        }
        console.log('✅ Total files after input selection:', this.files.length);
        return;
      }
    }

    console.log(
      '❌ Unhandled event type in onFileSelected:',
      typeof event,
      event
    );
  }

  // Add these methods to see what's happening
  onFileUploadEvent(event: any) {
    console.log('🟢 File upload event received:', event);
    this.onFileSelected(event);
  }

  onFilesChangedEvent(files: File[]) {
    console.log('🟢 Files changed event received:', files);
    this.onFilesChanged(files);
  }

  onCancel() {
    // Reset form submission state
    this.isFormSubmitted = false;
    this.appComService.updateIndexInfoResetToggle();

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
      this.fileUploadComponent.resetComponent();
    }

    // Trigger change detection
    this.cdr.detectChanges();

    const modalDiv = document.getElementById('modal-document-index');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  // Add this method for testing
  testAddFile() {
    const testContent = 'This is test content';
    const testFile = new File([testContent], 'test.txt', {
      type: 'text/plain',
    });
    this.files.push(testFile);
    console.log('🧪 Test file added, total files:', this.files.length);
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
