import { Component, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { ErrorMessageService } from '../../_services';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.css',
})
export class FileUploadComponent {
  @Input() filePathUri: string | null = null;
  @Input() displayMode: string | null = null;
  private url: string = environment.apiUrl + 'Document/';
  fileName = '';

  constructor(private http: HttpClient, private errorMessageService: ErrorMessageService) {}

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];
  
      if (file) {
        this.fileName = file.name;
        const formData = new FormData();
        formData.append('File', file);
        formData.append('FilePathUri', this.filePathUri || '');
        const upload$ = this.http.post(this.url + 'Upload', formData);
        upload$.subscribe({
          next: (response) => {
            // Handle successful upload
            // console.log('Upload successful', response);
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {

console.log('EMFTEST (UPLOAD) - error', error.error.errMessage);

              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          }
        });
      }
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
