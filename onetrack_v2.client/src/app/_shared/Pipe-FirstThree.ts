import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'firstThree'
  })
  export class FirstThreePipe implements PipeTransform {
    transform(value: string): string {
      let items = value.split('|');
      if (items.length > 2) {
        return items.slice(0, 3).join('|') + '...';
      } else {
        return value;
      }
    }
  }