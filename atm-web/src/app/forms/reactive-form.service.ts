import { FormBuilder, FormGroup, ValidatorFn, AbstractControl } from '@angular/forms';
import { Injectable } from '@angular/core';
import { FormField } from './form-field.model';

@Injectable({
    providedIn: 'root'
})
export class ReactiveFormService {
    formBuilder: FormBuilder = new FormBuilder();

    constructor() { }

    private initializeForm(form: FormGroup) {
        form = this.formBuilder.group({});
        return form;
    }

    createForm(form: FormGroup, formFields: FormField[], bypassValidator: boolean = false): FormGroup {
        form = this.initializeForm(form);
        if (!formFields || formFields.length === 0) {
            return form;
        }
        formFields.forEach(field => {
            if (field && field.controlName && (field.validators || bypassValidator)) {
                this.registerControl(form, field.defaultValue, field.controlName, field.validators || []);
            }
        });
        return form;
    }

    addFormFields(form: FormGroup, formFields: FormField[]): FormGroup {
        if (!form) {
            throw new Error('Form is null.');
        }
        if (formFields && formFields.length !== 0) {
            formFields.forEach(field => {
                if (field && field.controlName && field.validators) {
                    this.registerControl(form, field.defaultValue, field.controlName, field.validators);
                }
            });
            return form;
        }
        return form;
    }

    removeFormFields(form: FormGroup, formFields: FormField[]): FormGroup {
        if (!form) {
            throw new Error('Form is null.');
        }
        if (formFields && formFields.length !== 0) {
            formFields.forEach(field => {
                if (field && field.controlName) {
                    this.removeControl(form, field.controlName);
                }
            });
            return form;
        }
        return form;
    }

    createControl(defaultValue: any, validators: ValidatorFn[]): AbstractControl {
        return this.formBuilder.control(defaultValue, validators);
    }

    registerControl(form: FormGroup, defaultValue: any, controlName: string, validators: any[]): void {
        if (!Object.keys(form.controls).includes(controlName)) {
            const control = this.createControl(defaultValue, validators);
            if (!control) { throw new Error('Unable to create a form control.'); }
            this.addControl(form, controlName, control);
        }
    }

    addControl(form: FormGroup, controlName: string, control: AbstractControl): void {
        if (!form) { return; }
        form.addControl(controlName, control);
        form.get(controlName)?.updateValueAndValidity();
    }

    removeControl(form: FormGroup, controlName: string): void {
        if (!form) { return; }
        if (Object.keys(form.controls).includes(controlName)) {
            form.removeControl(controlName);
            form.updateValueAndValidity();
        }
    }

    getControlValue(form: FormGroup, controlName: string): any {
        if (!form) { return null; }
        if (!Object.keys(form.controls).includes(controlName)) {
            return null;
        }
        return form.get(controlName)?.value;
    }

    setControlValue(form: FormGroup, controlName: string, value: any, options?: any): void {
        if (!form) { return; }
        const control = form.get(controlName);
        if (control) {
            form.get(controlName)?.setValue(value, options);
            form.get(controlName)?.updateValueAndValidity();
        }
    }

    setValidators(form: FormGroup, controlName: string, validators?: any[]): void {
        if (!form) { return; }
        const control = form.get(controlName);
        if (control) {
            form.get(controlName)?.setValidators(validators || []);
            form.get(controlName)?.updateValueAndValidity();
        }
    }

    clearValidators(form: FormGroup, controlName: string): void {
        if (!form) { return; }
        const control = form.get(controlName);
        if (control) {
            form.get(controlName)?.clearValidators();
            form.get(controlName)?.updateValueAndValidity();
        }
    }

    resetForm(form: FormGroup): void {
        if (form) {
            form.reset();
        }
    }

    setError(form: FormGroup, controlName: string): any {
        if (!form) { return null; }
        if (!Object.keys(form.controls).includes(controlName)) {
            return null;
        }
        return form.controls[controlName].setErrors({'incorrect': true});
    }
}
