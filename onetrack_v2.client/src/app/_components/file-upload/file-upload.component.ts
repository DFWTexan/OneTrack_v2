import { Component, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.css',
})
export class FileUploadComponent {
  @Input() filePathUri: string = '';
  @Input() displayMode: string = 'ATTACHMENT';
  private url: string = environment.apiUrl + 'Misc/';
  fileName = '';

  constructor(private http: HttpClient) {}

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];
  
      if (file) {
        this.fileName = file.name;
        const formData = new FormData();
        formData.append('File', file);
        formData.append('FilePathUri', this.filePathUri);
        const upload$ = this.http.post(this.url + 'FileUpload', formData);
        upload$.subscribe();
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
