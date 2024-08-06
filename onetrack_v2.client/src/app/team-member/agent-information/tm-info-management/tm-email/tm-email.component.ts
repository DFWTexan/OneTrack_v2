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
import { Subscription } from 'rxjs';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

import {
  AgentInfo,
  EmailComTemplate,
  ManagerHierarchy,
} from '../../../../_Models';
import {
  AgentDataService,
  EmailDataService,
  ErrorMessageService,
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

  private subscriptions = new Subscription();

  ccEmail: string[] = [];
  chkMgr: boolean = false;
  chkDM: boolean = false;
  chkRD: boolean = false;
  emailSubject: string = '';
  emailFile: string = '';
  emailBody: string = '';

  constructor(
    private errorMessageService: ErrorMessageService,
    private emailDataService: EmailDataService,
    public agentDataService: AgentDataService,
    public userInfoDataService: UserAcctInfoDataService,
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
    emailSendItem.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (this.docSubType === '{MESSAGE}') {
      if (
        emailSendItem.emailSubject === '' ||
        emailSendItem.emailSubject === null
      ) {
        this.emailForm.controls['emailSubject'].setErrors({
          invalid: true,
        });
      }

      if (emailSendItem.emailBody === '' || emailSendItem.emailBody === null) {
        this.emailForm.controls['emailBody'].setErrors({
          invalid: true,
        });
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

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
