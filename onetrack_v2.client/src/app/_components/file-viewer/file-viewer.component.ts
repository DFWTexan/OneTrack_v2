import { Component, OnDestroy } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Subscription } from 'rxjs';

import { FileService } from '../../_services';

@Component({
  selector: 'app-file-viewer',
  templateUrl: './file-viewer.component.html',
  styleUrl: './file-viewer.component.css',
})
export class FileViewerComponent implements OnDestroy {
  fileContent: string = '';
  pdfSrc: SafeResourceUrl | null = null;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private fileService: FileService,
    private sanitizer: DomSanitizer
  ) {}

  loadFile(path: string, filename: string): void {
    this.subscriptionData.add(
      this.fileService.getFileContent(path, filename).subscribe({
        next: (content) => {
          this.fileContent = content;
        },
        error: (error) => {
          console.error('Error loading file:', error);
        },
      })
    );
  }

  viewPdfFile(path: string, filename: string): void {
    this.fileService.getFile(path, filename).subscribe(
      (blob) => {
        const url = URL.createObjectURL(blob);
        this.pdfSrc = this.sanitizer.bypassSecurityTrustResourceUrl(url);
      },
      (error) => {
        console.error('Error loading PDF file:', error);
      }
    );
  }

  downloadFile(path: string, filename: string): void {
    this.subscriptionData.add(
      this.fileService.getFile(path, filename).subscribe(
        (blob) => {
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', filename);
          document.body.appendChild(link);
          link.click();
          link.remove();
          window.URL.revokeObjectURL(url);
        },
        (error) => {
          console.error('Error downloading file:', error);
        }
      )
    );
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
