import {
  Component,
  ElementRef,
  Injectable,
  OnDestroy,
  OnInit,
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
import { AgentDataService, EmailDataService } from '../../../../_services';

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
    private emailDataService: EmailDataService,
    public agentDataService: AgentDataService,
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
    emailContent.to = this.agentInfo.email;
    emailContent.ccEmail = this.ccEmail;
    emailContent.subject = this.emailSubject;
    emailContent.emailTemplateID = this.selectedTemplate;
    emailContent.emailBody = this.emailBody;

    // console.log('EMFTEST (Email: onSubmit) - form => \n', form);
    console.log(
      'EMFTEST (Email: onSubmit) - this.emailBody => \n',
      this.emailBody
    );
    console.log('EMFTEST (Email: onSubmit) - emailContent => \n', emailContent);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
