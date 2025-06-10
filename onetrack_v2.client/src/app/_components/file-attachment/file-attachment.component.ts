import { Component, EventEmitter, Input, Output } from '@angular/core';

import { EmailDataService } from '../../_services';
import { environment } from '../../_environments/environment';

@Component({
  selector: 'app-file-attachment',
  templateUrl: './file-attachment.component.html',
  styleUrl: './file-attachment.component.css',
})
export class FileAttachmentComponent {
  @Input() uploadType: string | null = null;
  @Input() filePathUri: string | null = null;
  @Input() displayMode: string | null = null;
  @Input() isDisabled: boolean = false;
  @Output() filesChanged = new EventEmitter<File[]>();
  attachedFiles: File[] = [];
  private url: string = environment.apiUrl + 'Document/';
  fileName = '';

  constructor(private emailDataService: EmailDataService) {}

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];

      this.attachedFiles.push(file);

      this.emailDataService.setAttachedFiles(this.attachedFiles);

      this.filesChanged.emit(this.attachedFiles);

      target.value = '';
    }
  }

  deleteFile(index: number) {
    this.attachedFiles.splice(index, 1);
    this.emailDataService.setAttachedFiles(this.attachedFiles);
    this.filesChanged.emit(this.attachedFiles);
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
