import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-tm-email',
  templateUrl: './tm-email.component.html',
  styleUrl: './tm-email.component.css'
})
export class TmEmailComponent {


  onSubmit(form: NgForm) {
    console.log(form);
  }

}
