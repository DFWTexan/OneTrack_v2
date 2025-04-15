import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../_environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  private apiUrl: string = environment.apiUrl + 'Document/';
  filename: string | undefined;

  constructor(private http: HttpClient) {}

  getFileContent(path: string, filename: string): Observable<string> {
    const queryParams = `?path=${path}&filename=${filename}`;
    return this.http.get(`${this.apiUrl}GetFileContent${queryParams}`, {
      responseType: 'text',
    });
  }

  // getFile(path: string, filename: string): Observable<Blob> {
  //   const queryParams = `?path=${path}&filename=${filename}`;
  //   return this.http.get(`${this.apiUrl}GetFile${queryParams}`, {
  //     responseType: 'blob',
  //   });
  // }
  getFile(path: string, filename: string): Observable<Blob> {

console.log('EMFTEST () FileService getFile called with path: ', path, 'and filename:', filename);

    const queryParams = `?path=${encodeURIComponent(path)}&filename=${encodeURIComponent(filename)}`;
    return this.http.get(`${this.apiUrl}GetFile${queryParams}`, {
      responseType: 'blob',
    });
  }

  getFileDownload(filePath: string): Observable<Blob> {
    const queryParams = `?filePath=${filePath}`;
    return this.http.get(`${this.apiUrl}GetFileDownload${queryParams}`, {
      responseType: 'blob',
    });
  }

  downloadFile(filePath: string): void {
    if (filePath !== undefined) {
      const filename = filePath.split('/').pop() || 'default-filename';
      this.getFileDownload(filePath).subscribe(
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
      );
    } else {
      console.error('File path is undefined');
    }
  }
}
