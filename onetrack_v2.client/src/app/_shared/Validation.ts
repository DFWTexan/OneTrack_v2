import { AbstractControl } from '@angular/forms';

export function dateValidator(control: AbstractControl): { [key: string]: boolean } | null {
  const datePatternMMDDYYYY = /^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$/;
  const datePatternYYYYMMDD = /^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$/;
  if (control.value && !datePatternMMDDYYYY.test(control.value) && !datePatternYYYYMMDD.test(control.value)) {
    return { invalidFormat: true };
  }
  return null;
}