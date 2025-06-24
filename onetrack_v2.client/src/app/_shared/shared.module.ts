import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatBadgeModule } from '@angular/material/badge';

import { SharedRoutingModule } from './shared-routing.module';

import { ConfirmDialogComponent } from '../_components/confirm-dialog/confirm-dialog.component';
import { InfoDialogComponent } from '../_components/info-dialog/info-dialog.component';
import { ModalComponent } from '../_components/modal/modal.component';
import { PhoneFormatPipe } from '../_shared/Pipe-Phone-Format';
import { ZipFormatPipe } from '../_shared/Pipe-Zip-Format'; 
import { SsnFormatPipe } from '../_shared/Pipe-SSN-Format';
import { FirstThreePipe } from '../_shared/Pipe-FirstThree';
import { PaginationComponent } from '../_components/pagination/pagination.component';
import { EditLicenseComponent } from '../admin/license-edit/edit-license/edit-license.component';
import { FileUploadComponent } from '../_components/file-upload/file-upload.component';
import { FileViewerComponent } from '../_components/file-viewer/file-viewer.component';
import { FileAttachmentComponent } from '../_components/file-attachment/file-attachment.component';
import { ViewEmployeeCommunicationComponent } from '../_components/view-employee-communication/view-employee-communication.component';
import { IMaskModule } from 'angular-imask';

@NgModule({
  declarations: [
    ConfirmDialogComponent,
    InfoDialogComponent,
    ModalComponent,
    PhoneFormatPipe,
    ZipFormatPipe,
    SsnFormatPipe,
    FirstThreePipe,
    PaginationComponent,
    EditLicenseComponent,
    FileUploadComponent,
    FileViewerComponent,
    FileAttachmentComponent,
    ViewEmployeeCommunicationComponent,
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatButtonModule,
    MatSidenavModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatTabsModule,
    MatSelectModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatDialogModule,
    MatDividerModule,
    MatBadgeModule,
    IMaskModule,
  ],
  exports: [
    ConfirmDialogComponent,
    InfoDialogComponent,
    ModalComponent,
    PhoneFormatPipe,
    ZipFormatPipe,
    SsnFormatPipe,
    FirstThreePipe,
    PaginationComponent,
    EditLicenseComponent,
    FileUploadComponent,
    FileViewerComponent,
    FileAttachmentComponent,
    ViewEmployeeCommunicationComponent,
    MatButtonModule,
    MatSidenavModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatTabsModule,
    MatSelectModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatDialogModule,
    MatDividerModule,
    MatBadgeModule,
    FormsModule,
    IMaskModule,
  ],
})
export class SharedModule {}
