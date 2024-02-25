import { Component } from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';


@Component({
  selector: 'app-multi-select-ddl',
  templateUrl: './multi-select-ddl.component.html',
  styleUrl: './multi-select-ddl.component.css',
  standalone: true,
  imports: [MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule],
})
export class MultiSelectDdlComponent {
  agentStatuses = new FormControl('');
  statusList: string[] = ['ALL', 'Active', 'Pending', 'In-Active', 'Leave', 'Terminated'];
}
