import { Component, inject, OnInit } from '@angular/core';
import { FormComponent } from '../../forms/form.component';
import { FormGroup, Validators } from '@angular/forms';
import { ReactiveFormService } from '../../forms/reactive-form.service';
import { ReactiveFormValidationService } from '../../forms/reactive-form-validation.service';
import { AccountService } from '../account.service';
import { FormField } from '../../forms/form-field.model';
import { Account, DepositRequest, TransferRequest } from '../models/account.model';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, NativeDateAdapter } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrl: './deposit.component.scss',
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
export class DepositComponent extends FormComponent implements OnInit {

  depositForm: FormGroup = new FormGroup({});

  readonly FieldName: {
    toAccountId: string;
    amount: string;
  } = {
      toAccountId: 'toAccountId',
      amount: 'amount',
    };

  accounts?: Account[] = [<Account>{}];
  private _snackBar = inject(MatSnackBar);

  get toAccountIdHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.toAccountId);
  }

  get amountHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.amount);
  }

  private checkFormFieldError(fieldName: string): boolean {
    return !!this.depositForm && this.hasFormFieldError(this.depositForm, fieldName);
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

  onDepositClick(): void {
    if (!this.formValidationService.validateForm(this.depositForm)) {
      return;
    }
    this.processDeposit();
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  private createForm(): void {
    const formFields: FormField[] = [
      new FormField(null, this.FieldName.toAccountId, [Validators.required]),
      new FormField(null, this.FieldName.amount, [Validators.required]),
    ];
    this.depositForm = this.formService.createForm(this.depositForm, formFields);
  }

  private processDeposit(): void {
    const request = this.prepareDepositRequest();
    this.accountService.deposit(request.toAccountId, request.amount)
      .subscribe(response => {
        console.log(response);
        if (response) {
          this.openSnackBar(response.message, 'OK');
        }
      });
  }

  private prepareDepositRequest(): DepositRequest {
    const request = new DepositRequest();
    request.toAccountId = this.getFormFieldValue(this.FieldName.toAccountId);
    request.amount = +this.getFormFieldValue(this.FieldName.amount);
    return request;
  }

  private getFormFieldValue(fieldName: string): any {
    return this.formService.getControlValue(this.depositForm, fieldName);
  }

  private setFormFieldValue(fieldName: string, fieldValue: string): void {
    return this.formService.setControlValue(this.depositForm, fieldName, fieldValue);
  }

  private setDefaultFieldValues(): void {
    if (this.accounts && this.accounts.length !== 0) {
      const accountId = this.accounts[0].id;
      this.setFormFieldValue(this.FieldName.toAccountId, accountId);
    }
  }
}

