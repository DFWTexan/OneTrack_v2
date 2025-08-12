import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  Injectable,
  OnDestroy,
  OnInit,
  SecurityContext,
  ViewChild,
} from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { firstValueFrom, Subscription } from 'rxjs';
import {
  DomSanitizer,
  SafeHtml,
  SafeResourceUrl,
} from '@angular/platform-browser';
import * as mammoth from 'mammoth';

import {
  AgentInfo,
  EmailComTemplate,
  ManagerHierarchy,
} from '../../../../_Models';
import {
  AgentDataService,
  AppComService,
  EmailDataService,
  ErrorMessageService,
  FileService,
  UserAcctInfoDataService,
} from '../../../../_services';

@Component({
  selector: 'app-tm-email',
  templateUrl: './tm-email.component.html',
  styleUrl: './tm-email.component.css',
})
@Injectable()
export class TmEmailComponent implements OnInit, OnDestroy {
  // @ViewChild('container') container!: ElementRef;
  isSubmitted = false;
  emailForm!: FormGroup;
  isSubjectReadOnly = false;
  isTemplateFound = true;
  agentInfo: AgentInfo = {} as AgentInfo;
  docSubType: string = '{MESSAGE}';
  subject: string = '';
  // mgrHierarchy: ManagerHierarchy[] = [];
  emailTemplateID: number = 33;
  emailTemplate: string = 'APP-{Message}';
  emailComTemplates: EmailComTemplate[] = [];
  selectedTemplate: number = 33;
  htmlHeaderContent: SafeHtml = '' as SafeHtml;
  htmlFooterContent: SafeHtml = '' as SafeHtml;
  htmlContent: SafeHtml = '' as SafeHtml;
  FileDisplayMode = 'ATTACHMENT'; //--> CHOSEFILE / ATTACHMENT
  file: File | null = null;
  fileUri: string | null = null;
  uploadType: string = 'AttachmentLoc';
  pdfSrc: SafeResourceUrl | null = null;
  filePdfSrc: SafeResourceUrl | null = null;
  fileHtmlContent: SafeHtml | null = null;
  files: File[] = [];
  isDisplayDate: boolean = false;
  emailDate: Date | null = this.getNextBusinessDate();

  private subscriptions = new Subscription();

  ccEmail: string[] = [];
  chkMgr: boolean = false;
  chkDM: boolean = false;
  chkRD: boolean = false;
  emailSubject: string = '';
  emailFile: string = '';
  emailBody: string = '';
  emailAttachments: string[] = [];
  documentPath: string = '';

  constructor(
    private errorMessageService: ErrorMessageService,
    private emailDataService: EmailDataService,
    public agentDataService: AgentDataService,
    public appComService: AppComService,
    public userInfoDataService: UserAcctInfoDataService,
    private fileService: FileService,
    private sanitizer: DomSanitizer,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.emailForm = this.fb.group({
      communicationID: [33],
      emailSubject: [''],
      emailBody: [''],
      emailAttachment: [''],
      emailDate: [this.formatDateForInput(this.emailDate)],
    });

    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: AgentInfo) => {
          this.agentInfo = agentInfo;

          this.resetForm();
          this.emailAttachments = [];
          this.file = null;
          this.files = [];
          this.ccEmail = [];
          // this.chkMgr = false;
          // this.chkDM = false;
          // this.chkRD = false;
          // (document.getElementsByName('chkMgr')[0] as HTMLInputElement).checked = false;
          // (document.getElementsByName('chkDM')[0] as HTMLInputElement).checked = false;
          // (document.getElementsByName('chkRD')[0] as HTMLInputElement).checked = false;
          const chkMgr = document.getElementsByName('chkMgr')[0] as
            | HTMLInputElement
            | undefined;
          if (chkMgr) chkMgr.checked = false;
          const chkDM = document.getElementsByName('chkDM')[0] as
            | HTMLInputElement
            | undefined;
          if (chkDM) chkDM.checked = false;
          const chkRD = document.getElementsByName('chkRD')[0] as
            | HTMLInputElement
            | undefined;
          if (chkRD) chkRD.checked = false;
          this.cdr.detectChanges();
          this.cdr.detectChanges();

          this.subscriptions.add(
            this.emailDataService
              .fetchEmailComTemplateByID(33, this.agentInfo.employmentID)
              .subscribe((rawHtmlContent: any) => {
                // this.htmlContent =
                //   this.sanitizer.bypassSecurityTrustHtml(rawHtmlContent);
                this.htmlHeaderContent = this.sanitizer.bypassSecurityTrustHtml(
                  rawHtmlContent.header
                );
                this.htmlFooterContent = this.sanitizer.bypassSecurityTrustHtml(
                  rawHtmlContent.footer
                );
              })
          );
        }
      )
    );

    this.subscriptions.add(
      this.emailDataService
        .fetchEmailComTemplates()
        .subscribe((emailComTemplates: EmailComTemplate[]) => {
          this.emailComTemplates = emailComTemplates;
        })
    );

    this.subscriptions.add(
      this.emailDataService.attachedFilesChanged.subscribe((files: File[]) => {
        this.files = [...files];
        this.emailAttachments = files.map((file) => file.name);
      })
    );
  }

  onTemplateChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.emailTemplateID = +value;
    if (this.emailTemplateID === 128) {
      this.isDisplayDate = true;
      this.emailForm.patchValue({
        emailDate: this.formatDateForInput(this.getNextBusinessDate()),
      });
    } else {
      this.isDisplayDate = false;
    }

    this.htmlHeaderContent = '';
    this.htmlContent = '';
    this.htmlFooterContent = '';

    this.subscriptions.add(
      this.emailDataService
        .fetchEmailComTemplateByID(+value, this.agentInfo.employmentID, this.formatDateForAPI(this.emailDate))
        .subscribe((rawHtmlContent: any) => {
          this.docSubType = rawHtmlContent.docSubType;
          this.subject = rawHtmlContent.subject;
          this.isTemplateFound = rawHtmlContent.isTemplateFound;
          this.emailAttachments = rawHtmlContent.attachments || [];
          this.documentPath = rawHtmlContent.docAttachmentPath;

          // Fetch and create File objects for each attachment
          const attachmentPromises = (rawHtmlContent.attachments || []).map(
            async (attachment: string) => {
              const filePath = `${rawHtmlContent.docAttachmentPath}`;
              try {
                const blob = await firstValueFrom(
                  this.fileService.getFile(filePath, attachment)
                );
                if (blob) {
                  return new File([blob], attachment);
                } else {
                  throw new Error(`Failed to fetch file: ${attachment}`);
                }
              } catch (error) {
                console.error(`Error fetching file: ${attachment}`, error);
                throw error;
              }
            }
          );

          Promise.all(attachmentPromises).then((files) => {
            this.files = files; // Store valid File objects
            console.log('Files loaded:', this.files);
          });

          if (
            rawHtmlContent.docSubType === '{MESSAGE}' ||
            rawHtmlContent.docSubType === 'Incomplete' ||
            rawHtmlContent.docSubType === 'Employment History'
          ) {
            this.isSubjectReadOnly =
              rawHtmlContent.docSubType == '{MESSAGE}' ? false : true;
            this.emailForm.patchValue({
              emailSubject:
                rawHtmlContent.docSubType == '{MESSAGE}'
                  ? ''
                  : rawHtmlContent.subject,
            });
            this.htmlHeaderContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.header
            );
            this.htmlFooterContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.footer
            );
          } else {
            this.isSubjectReadOnly = true;
            this.emailForm.patchValue({
              emailSubject: rawHtmlContent.subject,
            });
            this.htmlContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.htmlContent
            );
            if (rawHtmlContent.docSubType === null) {
              this.emailForm.setErrors({ invalidForm: true });
            }
          }
        })
    );
  }

  onCcChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    if (target.checked) {
      this.ccEmail.push(value);
    } else {
      const index = this.ccEmail.indexOf(value);
      if (index > -1) {
        this.ccEmail.splice(index, 1);
      }
    }
  }

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];

      if (file) {
        this.emailFile = file.name;
        const formData = new FormData();
        formData.append('thumbnail', file);
        // const upload$ = this.emailDataService.uploadFile(formData);
        // upload$.subscribe();
      }
    }
  }

  onFilesChanged(files: File[]) {
    this.files = files;
  }

  onChangeDate(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.emailForm.patchValue({
      emailDate: value
    });

    const dateValue = value ? new Date(value + 'T00:00:00') : null;

    this.subscriptions.add(
      this.emailDataService
        .fetchEmailComTemplateByID(this.emailTemplateID, this.agentInfo.employmentID, this.formatDateForAPI(dateValue))
        .subscribe((rawHtmlContent: any) => {
          this.docSubType = rawHtmlContent.docSubType;
          this.subject = rawHtmlContent.subject;
          this.isTemplateFound = rawHtmlContent.isTemplateFound;
          this.emailAttachments = rawHtmlContent.attachments || [];
          this.documentPath = rawHtmlContent.docAttachmentPath;

          // Fetch and create File objects for each attachment
          const attachmentPromises = (rawHtmlContent.attachments || []).map(
            async (attachment: string) => {
              const filePath = `${rawHtmlContent.docAttachmentPath}`;
              try {
                const blob = await firstValueFrom(
                  this.fileService.getFile(filePath, attachment)
                );
                if (blob) {
                  return new File([blob], attachment);
                } else {
                  throw new Error(`Failed to fetch file: ${attachment}`);
                }
              } catch (error) {
                console.error(`Error fetching file: ${attachment}`, error);
                throw error;
              }
            }
          );

          Promise.all(attachmentPromises).then((files) => {
            this.files = files; // Store valid File objects
            console.log('Files loaded:', this.files);
          });

          if (
            rawHtmlContent.docSubType === '{MESSAGE}' ||
            rawHtmlContent.docSubType === 'Incomplete' ||
            rawHtmlContent.docSubType === 'Employment History'
          ) {
            this.isSubjectReadOnly =
              rawHtmlContent.docSubType == '{MESSAGE}' ? false : true;
            this.emailForm.patchValue({
              emailSubject:
                rawHtmlContent.docSubType == '{MESSAGE}'
                  ? ''
                  : rawHtmlContent.subject,
            });
            this.htmlHeaderContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.header
            );
            this.htmlFooterContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.footer
            );
          } else {
            this.isSubjectReadOnly = true;
            this.emailForm.patchValue({
              emailSubject: rawHtmlContent.subject,
            });
            this.htmlContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.htmlContent
            );
            if (rawHtmlContent.docSubType === null) {
              this.emailForm.setErrors({ invalidForm: true });
            }
          }
        })
    );
  }

  onSubmit() {
    let emailSendItem: any = this.emailForm.value;

    emailSendItem.employeeID = this.agentInfo.employeeID;
    emailSendItem.EmploymentID = this.agentInfo.employmentID;
    emailSendItem.CommunicationID = this.selectedTemplate;
    emailSendItem.EmailTo = this.agentInfo.email;
    emailSendItem.CcEmail = this.ccEmail;
    emailSendItem.EmailContent =
      this.docSubType === '{MESSAGE}'
        ? this.buildHTMLContent(this.emailForm.value.emailBody).toString()
        : this.htmlContent.toString();

    // Ensure files are valid File objects
    emailSendItem.fileAttachments = this.files.filter(
      (file) => file instanceof File
    );

    emailSendItem.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (this.docSubType === '{MESSAGE}') {
      if (!emailSendItem.emailSubject) {
        this.emailForm.controls['emailSubject'].setErrors({ invalid: true });
      }

      if (!emailSendItem.emailBody) {
        this.emailForm.controls['emailBody'].setErrors({ invalid: true });
      }
    }

    if (this.emailForm.invalid) {
      return;
    }

    this.subscriptions.add(
      this.emailDataService.sendEmail(emailSendItem).subscribe({
        next: (response) => {
          // Reset form submission state
          this.isSubmitted = false;

          // Reset the form to initial state
          this.resetForm();

          // Reset all component properties to initial state
          this.emailAttachments = [];
          this.file = null;
          this.files = [];
          this.ccEmail = [];
          this.selectedTemplate = 33;
          this.emailTemplateID = 33;

          // Reset template-related properties
          this.docSubType = '{MESSAGE}';
          this.subject = '';
          this.isSubjectReadOnly = false;
          this.isTemplateFound = true;
          this.htmlHeaderContent = '' as SafeHtml;
          this.htmlFooterContent = '' as SafeHtml;
          this.htmlContent = '' as SafeHtml;

          // Reset file viewer properties
          this.filePdfSrc = null;
          this.fileHtmlContent = null;
          this.documentPath = '';

          // Reset CC checkboxes
          const chkMgr = document.getElementsByName('chkMgr')[0] as
            | HTMLInputElement
            | undefined;
          if (chkMgr) chkMgr.checked = false;
          const chkDM = document.getElementsByName('chkDM')[0] as
            | HTMLInputElement
            | undefined;
          if (chkDM) chkDM.checked = false;
          const chkRD = document.getElementsByName('chkRD')[0] as
            | HTMLInputElement
            | undefined;
          if (chkRD) chkRD.checked = false;

          // Clear attached files in email service
          this.emailDataService.setAttachedFiles([]);

          // Trigger change detection
          this.cdr.detectChanges();

          this.appComService.updateAppMessage('Email Sent successfully.');

          this.agentDataService
            .fetchAgentInformation(
              this.agentDataService.agentInformation.employeeID
            )
            .subscribe((agentInfo: AgentInfo) => {
              this.agentDataService.agentInfoChanged.next(agentInfo);
            });
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.resetForm();
          }
        },
      })
    );
  }

  resetForm() {
    this.emailForm.reset();
    this.emailAttachments = [];
    this.emailForm.patchValue({
      communicationID: 33,
    });
  }

  private buildHTMLContent(vBody: string): string {
    let headerContent =
      this.sanitizer.sanitize(SecurityContext.HTML, this.htmlHeaderContent) ||
      '';
    let footerContent =
      this.sanitizer.sanitize(SecurityContext.HTML, this.htmlFooterContent) ||
      '';

    let htmlContent = headerContent;
    htmlContent += '<div>' + vBody + '</div>';
    htmlContent += footerContent;

    return htmlContent;
  }

  // onOpenDocument(url: string) {
  //   this.subscriptions.add(
  //     this.appComService.openDocument(url).subscribe({
  //       next: (response: Blob) => {
  //         const blobUrl = URL.createObjectURL(response);
  //         window.open(blobUrl, '_blank');
  //       },
  //       error: (error) => {
  //         this.errorMessageService.setErrorMessage(error.message);
  //       },
  //     })
  //   );
  // }

  removeAttachment(index: number): void {
    if (index >= 0 && index < this.emailAttachments.length) {
      const fileName = this.emailAttachments[index];
      this.emailAttachments.splice(index, 1);
      this.files = this.files.filter((file) => file.name !== fileName);
      this.emailDataService.deleteAttachedFile(index);
    }
  }

  viewFile(path: string, filename: string): void {
    this.fileService.getFile(path, filename).subscribe(
      (blob) => {
        const fileExtension = filename.split('.').pop()?.toLowerCase();

        // Reset previous content
        this.filePdfSrc = null;
        this.fileHtmlContent = null;
        if (fileExtension === 'pdf') {
          const url = URL.createObjectURL(blob);
          this.filePdfSrc = this.sanitizer.bypassSecurityTrustResourceUrl(url);
        } else if (fileExtension === 'docx') {
          const reader = new FileReader();
          reader.onload = async () => {
            const arrayBuffer = reader.result as ArrayBuffer;
            try {
              const result = await mammoth.convertToHtml({ arrayBuffer });
              this.fileHtmlContent = this.sanitizer.bypassSecurityTrustHtml(
                result.value
              );
            } catch (error) {
              console.error('Error converting .docx file:', error);
            }
          };
          reader.readAsArrayBuffer(blob);
        } else {
          console.error('Unsupported file type:', fileExtension);
        }
      },
      (error) => {
        console.error('Error loading file:', error);
      }
    );
  }

  closeViewer(): void {
    // this.pdfSrc = null;
    this.filePdfSrc = null;
    this.fileHtmlContent = null;
  }

  private getNextBusinessDate(): Date {
    const today = new Date();
    let nextBusinessDate = new Date(today);
    nextBusinessDate.setDate(today.getDate() + 1);

    // Skip weekends (Saturday = 6, Sunday = 0)
    while (nextBusinessDate.getDay() === 0 || nextBusinessDate.getDay() === 6) {
      nextBusinessDate.setDate(nextBusinessDate.getDate() + 1);
    }

    return nextBusinessDate;
  }

  private formatDateForInput(date: Date | null): string | null {
    if (!date) return null;
    
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    
    return `${year}-${month}-${day}`;
  }

  private formatDateForAPI(date: Date | null): string | null {
    if (!date) return null;
    
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const year = date.getFullYear();
    
    return `${month}-${day}-${year}`;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
