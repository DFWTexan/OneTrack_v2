import { Component, Injectable } from '@angular/core';

import { AppComService } from '../../_services';

@Component({
  selector: 'app-side-menu-nav',
  templateUrl: './side-menu-nav.component.html',
  styleUrl: './side-menu-nav.component.css'
})
@Injectable()
export class SideMenuNavComponent {

  constructor(public appComService: AppComService) { }

  ngOnInit() {
  }

  // toggleTickler() {
  //   this.tickleToggle = !this.tickleToggle;
  //   this.tickleToggleChanged.next(this.tickleToggle);
  // }

}
