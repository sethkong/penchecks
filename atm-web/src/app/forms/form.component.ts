import { ReactiveFormValidationService } from './reactive-form-validation.service';
import { FormGroup } from '@angular/forms';

export class FormComponent {
    private formValidation: ReactiveFormValidationService = new ReactiveFormValidationService();

    displayFormFieldError(form: FormGroup, field: string): any {
        return {
            'is-invalid': this.hasFormFieldError(form, field)
                || this.hasFormFieldPatternError(form, field)
        };
    }

    hasFormFieldError(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasRequiredError(form, field);
        }
        return false;
    }

    hasFormFieldMinimumAgeError(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasMinimumAgeError(form, field);
        }
        return false;
    }

    hasFormFieldDirty(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasRequiredDirty(form, field);
        }
        return false;
    }

    hasFormFieldPatternError(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasPatternError(form, field)
                && !this.formValidation.hasRequiredError(form, field);
        }
        return false;
    }

    hasFormFieldMaxLengthError(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasMaxLengthError(form, field)
                && !this.formValidation.hasRequiredError(form, field);
        }
        return false;
    }

    hasFormFieldMinLengthError(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasMinLengthError(form, field)
                && !this.formValidation.hasRequiredError(form, field);
        }
        return false;
    }

    hasFormFieldEmailError(form: FormGroup, field: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasEmailError(form, field)
                && !this.formValidation.hasRequiredError(form, field);
        }
        return false;
    }

    hasPasswordMatchError(form: FormGroup, field: string, otherField: string): boolean {
        if (form && this.formValidation) {
            return this.formValidation.hasPasswordMatchError(form, field, otherField)
                && !this.formValidation.hasRequiredError(form, field);
        }
        return false;
    }
}