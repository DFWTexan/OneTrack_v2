import { Component } from '@angular/core';

@Component({
  selector: 'app-quick-find',
  templateUrl: './quick-find.component.html',
  styleUrl: './quick-find.component.css',
})
export class QuickFindComponent {
  tmNumber: string = '';
  agentName: string = '';

  isByTMDisabled: boolean = false;
  isByAgentNameDisabled: boolean = false;

  constructor() {}

  onChangeByTmNumber() {
    this.isByTMDisabled = false;
    if (this.tmNumber.length > 0) {
      this.isByAgentNameDisabled = true;
    } else {
      this.isByAgentNameDisabled = false;
    }
  }

  onChangeByAgentName() {
    this.isByTMDisabled = true;
    if (this.agentName.length > 0) {
      this.isByTMDisabled = true;
    } else {
      this.isByTMDisabled = false;
    }
  }
}
