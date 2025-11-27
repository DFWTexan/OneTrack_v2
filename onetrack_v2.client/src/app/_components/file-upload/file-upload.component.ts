import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../_environments/environment';
import { EmailDataService, ErrorMessageService } from '../../_services';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.css',
})
export class FileUploadComponent implements OnChanges {
  @Input() uploadType: string | null = null;
  @Input() filePathUri: string | null = null;
  @Input() displayMode: string | null = null;
  @Input() isDisabled: boolean = false;
  @Input() resetFiles: boolean = false; // New input property
  @Output() filesChanged = new EventEmitter<File[]>();
  @Output() fullFilePathUri = new EventEmitter<string>();
  attachedFiles: File[] = [];
  private url: string = environment.apiUrl + 'Document/';
  fileName = '';

  constructor(
    private http: HttpClient,
    private errorMessageService: ErrorMessageService,
    private emailDataService: EmailDataService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['resetFiles'] && changes['resetFiles'].currentValue === true) {
      this.clearFiles();
    }
  }

  clearFiles(): void {
    this.attachedFiles = [];
    this.fileName = '';
    this.filePathUri = null;
    this.emailDataService.setAttachedFiles(this.attachedFiles);
    this.filesChanged.emit(this.attachedFiles);
  }

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];

      if (file) {
        this.fileName = file.name;
        this.attachedFiles.push(file);

        // Emit the files array immediately when file is selected
        this.filesChanged.emit([...this.attachedFiles]);

        // Also update the email service
        this.emailDataService.setAttachedFiles(this.attachedFiles);

        const formData = new FormData();
        formData.append('file', file);
        formData.append('fileName', this.fileName);
        formData.append('filePathType', this.uploadType || '');

        interface UploadResponse {
          objData: { fullPath: string };
          [key: string]: any;
        }

        const upload$ = this.http.post<UploadResponse>(
          this.url + 'Upload',
          formData
        );
        upload$.subscribe({
          next: (response) => {
            // Handle successful upload
            this.filePathUri = response.objData.fullPath;
            this.fullFilePathUri.emit(this.filePathUri);

            // Emit files again after successful upload to ensure consistency
            this.filesChanged.emit([...this.attachedFiles]);
          },
          error: (error) => {
            // If upload fails, remove the file from attachedFiles
            const fileIndex = this.attachedFiles.findIndex(
              (f) => f.name === file.name
            );
            if (fileIndex > -1) {
              this.attachedFiles.splice(fileIndex, 1);
              this.filesChanged.emit([...this.attachedFiles]);
              this.emailDataService.setAttachedFiles(this.attachedFiles);
            }

            if (error.error && error.error.errMessage) {
              console.log('EMFTEST (UPLOAD) - error', error.error.errMessage);
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          },
        });
      }

      target.value = '';
    }
  }

  onFileChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      // Correctly access files from the target variable
      const file = target.files[0];
      // Now you can use the file variable as needed
    }
  }

  deleteFile(index: number) {
    this.attachedFiles.splice(index, 1);
    this.emailDataService.setAttachedFiles(this.attachedFiles);
    this.filesChanged.emit(this.attachedFiles);
  }

  // Add this public method to FileUploadComponent
  resetComponent(): void {
    this.attachedFiles = [];
    this.fileName = '';
    this.filePathUri = null;
    this.emailDataService.setAttachedFiles(this.attachedFiles);
    this.filesChanged.emit(this.attachedFiles);
  }
}
