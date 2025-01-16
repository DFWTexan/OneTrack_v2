import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'PhoneFormat'})
export class PhoneFormatPipe implements PipeTransform {
  transform(rawNum: string) {
    let formattedNum = rawNum;
    if (rawNum && rawNum.length == 10) {
      formattedNum = `(${rawNum.substr(0, 3)}) ${rawNum.substr(3, 3)}-${rawNum.substr(6)}`;
    }
    return formattedNum;
  }
}