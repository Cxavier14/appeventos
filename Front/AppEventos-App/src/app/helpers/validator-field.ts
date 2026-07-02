import { AbstractControl, UntypedFormGroup } from "@angular/forms";

export class ValidatorField {
  static MustMatch(controlName: string, matchingcontrolName: string): any {
    return (group: AbstractControl) => {
      const formGroup = group as UntypedFormGroup;
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingcontrolName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        return null;
      }

      if (control.value != matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }

      return null;
    };
  }
}
