import { Injectable } from '@angular/core';
import { FormGroup, FormControl, AbstractControl, FormBuilder } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class ReactiveFormValidationService {
    inProgress = false;
    constructor() { }

    clearControlValidators(form: FormGroup, controlName: string): void {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            if (control) {
                form.get(controlName)?.clearValidators();
                form.get(controlName)?.updateValueAndValidity();
            }
        }
    }

    setControlValidators(form: FormGroup, controlName: string, validators: any[]): void {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            if (control) {
                form.get(controlName)?.setValidators(validators);
                form.get(controlName)?.updateValueAndValidity();
            }
        }
    }

    hasRequiredDirty(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            const hasRequiredDirty = control?.dirty || false;

            return control && !control.valid && control.touched && hasRequiredDirty;
        }
        return false;
    }

    hasRequiredError(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            const hasRequiredError = control?.errors?.hasOwnProperty('required') || false;

            return control && !control.valid && control.touched
                && hasRequiredError;
        }
        return false;
    }

    hasPatternError(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            const hasPatternError = control?.errors?.hasOwnProperty('pattern') || false;

            return control && !control.valid && control.touched && hasPatternError;
        }
        return false;
    }

    hasEmailError(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            const hasEmailError = control?.errors?.hasOwnProperty('email') || false;

            return control && !control.valid && control.touched && hasEmailError;
        }
        return false;
    }

    hasMaxLengthError(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            const hasMaxLengthError = control?.errors?.hasOwnProperty('maxlength') || false;

            return control && !control.valid && control.touched && hasMaxLengthError;
        }
        return false;
    }

    hasMinLengthError(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            const hasMinLengthError = control?.errors?.hasOwnProperty('minlength') || false;

            return control && !control.valid && control.touched && hasMinLengthError;
        }
        return false;
    }

    private getControl(form: FormGroup, controlName: string): AbstractControl {
        if (!form) {
            throw new Error('Form is undefined.');
        }
        const control = form.get(controlName);
        if (!control) {
            throw new Error(`Control of ${controlName} is null.`);
        }
        return control;
    }

    validateFormControls(form: FormGroup): FormGroup {
        if (!form) { return new FormBuilder().group({}); }

        Object.keys(form.controls).forEach(controlName => {
            const control = form.get(controlName);
            if (control && control instanceof FormControl) {
                control.markAsTouched({ onlySelf: true });
            } else if (control && control instanceof FormGroup) {
                this.validateFormControls(control);
            }
        });

        return form;
    }

    validateForm(form: FormGroup): boolean {
        this.inProgress = true;
        form = this.validateFormControls(form);
        if (!form.valid) {
            this.inProgress = false;
            return false;
        }
        return true;
    }

    hasPasswordMatchError(form: FormGroup, confirmPassword: string, password: string): boolean {
        if (form.get(confirmPassword)?.value && form.get(password)?.value)
            return form.get(confirmPassword)?.value !== form.get(password)?.value;
        return false;
    }

    hasMinimumAgeError(form: FormGroup, controlName: string): boolean {
        if (form && form.contains(controlName)) {
            const control = this.getControl(form, controlName);
            let isMinimumAge: boolean = false;
            isMinimumAge = (new Date().getFullYear() - new Date(control.value).getFullYear()) >= 18;
            return !isMinimumAge;
        }
        return false;
    }
}