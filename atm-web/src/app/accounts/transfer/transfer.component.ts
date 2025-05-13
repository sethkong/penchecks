import { Component, inject, OnInit } from '@angular/core';
import { FormComponent } from '../../forms/form.component';
import { FormGroup, Validators } from '@angular/forms';
import { ReactiveFormService } from '../../forms/reactive-form.service';
import { ReactiveFormValidationService } from '../../forms/reactive-form-validation.service';
import { AccountService } from '../account.service';
import { FormField } from '../../forms/form-field.model';
import { Account, TransferRequest } from '../models/account.model';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, NativeDateAdapter } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrl: './transfer.component.scss',
  providers: [
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'outline', floatLabel: 'always' }
    },
    { provide: DateAdapter, useClass: NativeDateAdapter },
    { provide: MAT_DATE_LOCALE, useValue: 'en-US' },
    {
      provide: MAT_DATE_FORMATS,
      useValue: {
        display: {
          dateInput: 'DD/MM/YYYY',
          monthYearLabel: 'MMM YYYY',
          dateA11yLabel: 'MMMM d, YYYY',
          monthYearA11yLabel: 'MMMM YYYY',
        }
      }
    },
  ]
})
export class TransferComponent extends FormComponent implements OnInit {
  transferForm: FormGroup = new FormGroup({});
  readonly FieldName: {
    fromAccountId: string;
    toAccountId: string;
    amount: string;
    transferAt: string;
    notes: string;
  } = {
      fromAccountId: 'fromAccountId',
      toAccountId: 'toAccountId',
      amount: 'amount',
      transferAt: 'transferAt',
      notes: 'notes',
    };
  accounts?: Account[] = [<Account>{}];
  private _snackBar = inject(MatSnackBar);

  get fromAccountIdHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.fromAccountId);
  }

  get toAccountIdHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.toAccountId);
  }

  get amountHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.amount);
  }

  get transferAtHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.transferAt);
  }

  private checkFormFieldError(fieldName: string): boolean {
    return !!this.transferForm && this.hasFormFieldError(this.transferForm, fieldName);
  }

  constructor(private formService: ReactiveFormService,
    private formValidationService: ReactiveFormValidationService,
    private accountService: AccountService
  ) {
    super();
    this.createForm();
  }

  ngOnInit(): void {
    this.accountService.getAccounts().subscribe(response => {
      this.accounts = response.data;
      console.log('Accounts: ', this.accounts);
      this.setDefaultFieldValues();
    });
  }

  onTransferClick(): void {
    if (!this.formValidationService.validateForm(this.transferForm)) {
      return;
    }
    this.processTransfer();
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  private createForm(): void {
    const formFields: FormField[] = [
      new FormField(null, this.FieldName.fromAccountId, [Validators.required]),
      new FormField(null, this.FieldName.toAccountId, [Validators.required]),
      new FormField(null, this.FieldName.amount, [Validators.required]),
      new FormField(null, this.FieldName.transferAt, [Validators.required]),
      new FormField(null, this.FieldName.notes, []),
    ];
    this.transferForm = this.formService.createForm(this.transferForm, formFields);
  }

  private processTransfer(): void {
    const request = this.prepareTransferRequest();
    this.accountService.transfer(request).subscribe(response => {
      console.log(response);
      if (response) {
        this.openSnackBar(response.message, 'OK');
      }
    });
  }

  private prepareTransferRequest(): TransferRequest {
    const request = new TransferRequest();
    request.fromAccountId = this.getFormFieldValue(this.FieldName.fromAccountId);
    request.toAccountId = this.getFormFieldValue(this.FieldName.toAccountId);
    request.amount = +this.getFormFieldValue(this.FieldName.amount);
    request.transferAt = this.getFormFieldValue(this.FieldName.transferAt);
    request.notes = this.getFormFieldValue(this.FieldName.notes);
    return request;
  }

  private getFormFieldValue(fieldName: string): any {
    return this.formService.getControlValue(this.transferForm, fieldName);
  }

  private setFormFieldValue(fieldName: string, fieldValue: string): void {
    return this.formService.setControlValue(this.transferForm, fieldName, fieldValue);
  }

  private setDefaultFieldValues(): void {
    if (this.accounts && this.accounts.length === 1) {
      const accountId = this.accounts[0].id;
      this.setFormFieldValue(this.FieldName.fromAccountId, accountId);
      this.setFormFieldValue(this.FieldName.toAccountId, accountId);
    } else if (this.accounts && this.accounts.length > 1) {
      const fromAccountId = this.accounts[0].id;
      const toAccountId = this.accounts[1].id;
      this.setFormFieldValue(this.FieldName.fromAccountId, fromAccountId);
      this.setFormFieldValue(this.FieldName.toAccountId, toAccountId);
    }
  }
}
