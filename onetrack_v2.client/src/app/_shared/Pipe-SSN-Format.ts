import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'ssnFormat'})
export class SsnFormatPipe implements PipeTransform {
  transform(rawNum: string) {
    let formattedNum = rawNum;
    if (rawNum && rawNum.length == 9) {
      formattedNum = `${rawNum.substr(0, 3)}-${rawNum.substr(3, 2)}-${rawNum.substr(5)}`;
    }
    return formattedNum;
  }
}