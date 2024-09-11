import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'ZipFormat'})
export class ZipFormatPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) {
      return 'N/A';
    }

    // Ensure the value is a string
    value = value.toString();

    // Format the phone number
    if (value.length === 9) {
      return `${value.slice(0, 5)}-${value.slice(5)}`;
    }

    return value;
  }
}
