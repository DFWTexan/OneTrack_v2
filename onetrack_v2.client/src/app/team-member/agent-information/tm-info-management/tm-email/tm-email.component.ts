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
  styleUrls: ['./tm-email.component.css'],
})
@Injectable()
export class TmEmailComponent implements OnInit, OnDestroy {
  isSubmitted: boolean = false;
  emailForm!: FormGroup;
  agentInfo: AgentInfo = {} as AgentInfo;
  docSubType: string = '{MESSAGE}';
  subject: string = '';
  emailTemplateID: number = 33;
  emailTemplate: string = 'APP-{Message}';
  emailComTemplates: EmailComTemplate[] = [];
  selectedTemplate: number = 33;
  htmlHeaderContent: SafeHtml = '' as SafeHtml;
  htmlFooterContent: SafeHtml = '' as SafeHtml;
  htmlContent: SafeHtml = '' as SafeHtml;

  private subscriptions = new Subscription();

  ccEmail: string[] = [];
  chkMgr: boolean = false;
  chkDM: boolean = false;
  chkRD: boolean = false;
  emailSubject: string = '';
  emailFile: File | null = null; // Changed type to File to handle file inputs
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
      emailFile: [null], // Initialize with null
    });

    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: AgentInfo) => {
          this.agentInfo = agentInfo;

          this.subscriptions.add(
            this.emailDataService
              .fetchEmailComTemplateByID(33, this.agentInfo.employmentID)
              .subscribe((rawHtmlContent: any) => {
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

    this.subscriptions.add(
      this.emailDataService
        .fetchEmailComTemplateByID(+value, this.agentInfo.employmentID)
        .subscribe((rawHtmlContent: any) => {
          this.docSubType = rawHtmlContent.docSubType;
          this.subject = rawHtmlContent.subject;

          if (this.docSubType === '{MESSAGE}') {
            this.htmlHeaderContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.header
            );
            this.htmlFooterContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.footer
            );
          } else {
            this.htmlContent = this.sanitizer.bypassSecurityTrustHtml(
              rawHtmlContent.htmlContent
            );
            if (rawHtmlContent.docSubType === null) {
              this.emailForm.setErrors({ invalidForm: true });
            }
          }
        })
    );

    const emailSubjectControl = this.emailForm.get('emailSubject');
    if (emailSubjectControl) {
      if (this.docSubType === '{MESSAGE}') {
        emailSubjectControl.disable();
      } else {
        emailSubjectControl.enable();
        this.emailForm.patchValue({
          emailSubject: this.subject,
        });
      }
    }
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

  onFileChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      this.emailFile = target.files[0];
      this.emailForm.patchValue({
        emailFile: this.emailFile,
      });
    }
  }

  // onSubmit() {
  //   let emailSendItem: any = this.emailForm.value;

  //   emailSendItem.employeeID = this.agentInfo.employeeID;
  //   emailSendItem.EmploymentID = this.agentInfo.employmentID;
  //   emailSendItem.CommunicationID = this.selectedTemplate;
  //   emailSendItem.EmailTo = this.agentInfo.email;
  //   emailSendItem.CcEmail = this.ccEmail;
  //   emailSendItem.EmailContent =
  //     this.docSubType === '{MESSAGE}'
  //       ? this.buildHTMLContent(this.emailForm.value.emailBody).toString()
  //       : this.htmlContent.toString();
  //   emailSendItem.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

  //   if (this.emailForm.invalid) {
  //     return;
  //   }

  //   this.subscriptions.add(
  //     this.emailDataService.sendEmail(emailSendItem).subscribe({
  //       next: (response) => {
  //         this.isSubmitted = false;
  //         this.resetForm();
  //         alert('Email sent successfully');
  //       },
  //       error: (error) => {
  //         if (error.error && error.error.errMessage) {
  //           this.errorMessageService.setErrorMessage(error.error.errMessage);
  //           this.resetForm();
  //         }
  //       },
  //     })
  //   );
  // }

  onSubmit() {
    if (this.emailForm.invalid) {
      return;
    }

    const formData = new FormData();
    formData.append('employeeID', this.agentInfo.employeeID.toString());
    formData.append('EmploymentID', this.agentInfo.employmentID.toString());
    // Use form control value for CommunicationID instead of selectedTemplate
    formData.append(
      'CommunicationID',
      this.emailForm.value.communicationID.toString()
    );
    formData.append('EmailTo', this.agentInfo.email);
    // Use form control value for CcEmail if it exists or default to JSON stringified this.ccEmail
    formData.append(
      'CcEmail',
      this.emailForm.value.ccEmail
        ? JSON.stringify(this.emailForm.value.ccEmail)
        : JSON.stringify(this.ccEmail)
    );
    // Use form control value for EmailContent
    formData.append(
      'EmailContent',
      this.docSubType === '{MESSAGE}'
        ? this.buildHTMLContent(this.emailForm.value.emailBody).toString()
        : this.htmlContent.toString()
    );
    formData.append('UserSOEID', this.userInfoDataService.userAcctInfo.soeid);

    // For the file input element
    const fileInput: HTMLInputElement | null =
      document.querySelector('#formFile');
    if (fileInput && fileInput.files && fileInput.files.length > 0) {
      formData.append('Attachment', fileInput.files[0]);
    }

    this.subscriptions.add(
      this.emailDataService.sendEmail(formData).subscribe({
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
    this.emailFile = null; // Reset file
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
