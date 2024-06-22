import {
  Component,
  ElementRef,
  Injectable,
  OnDestroy,
  OnInit,
  SecurityContext,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
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
  @ViewChild('container') container!: ElementRef;
  isSubmitted = false;
  agentInfo: AgentInfo = {} as AgentInfo;
  // mgrHierarchy: ManagerHierarchy[] = [];
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
  emailFile: string = '';
  emailBody: string = '';

  constructor(
    private errorMessageService: ErrorMessageService,
    private emailDataService: EmailDataService,
    public agentDataService: AgentDataService,
    public userInfoDataService: UserAcctInfoDataService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
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

    this.subscriptions.add(
      this.emailDataService
        .fetchEmailComTemplateByID(+value, this.agentInfo.employmentID)
        .subscribe((rawHtmlContent: string) => {
          // this.htmlContent = template;
          this.htmlContent =
            this.sanitizer.bypassSecurityTrustHtml(rawHtmlContent);
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

  onSubmit(form: NgForm) {
    let emailContent: any = {};

    emailContent.EmailTemplateID = this.selectedTemplate;
    emailContent.EmailTo = this.agentInfo.email;
    emailContent.CcEmail = this.ccEmail;
    emailContent.Subject = this.emailSubject;
    emailContent.EmailContent =
      this.selectedTemplate == 33
        ? this.buildHTMLContent().toString()
        : this.htmlContent.toString();
    emailContent.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    this.subscriptions.add(
      this.emailDataService.sendEmail(emailContent).subscribe({
        next: (response) => {
          this.isSubmitted = false;
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }
        },
      })
    );
  }

  private buildHTMLContent(): string {
    let headerContent =
      this.sanitizer.sanitize(SecurityContext.HTML, this.htmlHeaderContent) ||
      '';
    let footerContent =
      this.sanitizer.sanitize(SecurityContext.HTML, this.htmlFooterContent) ||
      '';

    let htmlContent = headerContent;
    htmlContent += '<div>' + this.emailBody + '</div>';
    htmlContent += footerContent;

    return htmlContent;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
