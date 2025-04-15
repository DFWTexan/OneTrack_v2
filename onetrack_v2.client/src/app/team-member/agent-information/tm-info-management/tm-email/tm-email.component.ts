import {
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
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.emailForm = this.fb.group({
      communicationID: [33],
      emailSubject: [''],
      emailBody: [''],
      emailAttachment: [''],
    });

    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: AgentInfo) => {
          this.agentInfo = agentInfo;

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

    this.htmlHeaderContent = '';
    this.htmlContent = '';
    this.htmlFooterContent = '';

    this.subscriptions.add(
      this.emailDataService
        .fetchEmailComTemplateByID(+value, this.agentInfo.employmentID)
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
                const blob = await firstValueFrom(this.fileService.getFile(filePath, attachment));
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

    console.log('EMFTEST (TM EMAIL: onSubmit) - this.files => \n', this.files);

    // Ensure files are valid File objects
    emailSendItem.fileAttachments = this.files.filter(
      (file) => file instanceof File
    );

    console.log(
      'EMFTEST (TM EMAIL: onSubmit) - emailSendItem.fileAttachments => \n',
      emailSendItem.fileAttachments
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
          this.isSubmitted = false;
          this.resetForm();
          alert('Email sent successfully');
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

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
