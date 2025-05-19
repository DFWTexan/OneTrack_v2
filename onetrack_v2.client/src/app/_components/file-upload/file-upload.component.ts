import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../_environments/environment';
import { EmailDataService, ErrorMessageService } from '../../_services';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.css',
})
export class FileUploadComponent {
  @Input() uploadType: string | null = null;
  @Input() filePathUri: string | null = null;
  @Input() displayMode: string | null = null;
  @Input() isDisabled: boolean = false;
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

 onFileSelected(event: any) {
  const target = event.target as HTMLInputElement;
  if (target.files && target.files.length) {
    const file: File = target.files[0];

    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append('file', file); // must match [FromForm] IFormFile file
      formData.append('fileName', this.fileName); // must match [FromForm] string fileName
      formData.append('filePathType', this.uploadType || ''); // must match [FromForm] string filePathType

      console.log('EMFTEST (UPLOAD) - formData => \n', formData);

      interface UploadResponse {
        objData: { fullPath: string };
        [key: string]: any;
      }

      const upload$ = this.http.post<UploadResponse>(this.url + 'Upload', formData);
      upload$.subscribe({
        next: (response) => {
          // Handle successful upload
          console.log('EMFTEST (UPLOAD: onFileSelected) - response => \n', response);

          this.filePathUri = response.objData.fullPath;
          this.fullFilePathUri.emit(this.filePathUri);
        },
        error: (error) => {
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
}
