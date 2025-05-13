import { Component, inject, OnInit } from '@angular/core';
import { FormComponent } from '../../forms/form.component';
import { FormGroup, Validators } from '@angular/forms';
import { ReactiveFormService } from '../../forms/reactive-form.service';
import { ReactiveFormValidationService } from '../../forms/reactive-form-validation.service';
import { AccountService } from '../account.service';
import { FormField } from '../../forms/form-field.model';
import { Account, DepositRequest, TransferRequest, WithdrawRequest } from '../models/account.model';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, NativeDateAdapter } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.component.html',
  styleUrl: './withdraw.component.scss',
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
export class WithdrawComponent extends FormComponent implements OnInit {
  withdrawForm: FormGroup = new FormGroup({});

  readonly FieldName: {
    fromAccountId: string;
    amount: string;
  } = {
      fromAccountId: 'fromAccountId',
      amount: 'amount',
    };

  accounts?: Account[] = [<Account>{}];
  private _snackBar = inject(MatSnackBar);

  get fromAccountIdHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.fromAccountId);
  }

  get amountHasRequiredError(): boolean {
    return this.checkFormFieldError(this.FieldName.amount);
  }

  private checkFormFieldError(fieldName: string): boolean {
    return !!this.withdrawForm && this.hasFormFieldError(this.withdrawForm, fieldName);
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

  onWithdrawClick(): void {
    if (!this.formValidationService.validateForm(this.withdrawForm)) {
      return;
    }
    this.processWithdraw();
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
      new FormField(null, this.FieldName.amount, [Validators.required]),
    ];
    this.withdrawForm = this.formService.createForm(this.withdrawForm, formFields);
  }

  private processWithdraw(): void {
    const request = this.prepareWithdrawRequest();
    this.accountService.withdraw(request.fromAccountId, request.amount)
      .subscribe(response => {
        console.log(response);
        if (response) {
          this.openSnackBar(response.message, 'OK');
        }
      });
  }

  private prepareWithdrawRequest(): WithdrawRequest {
    const request = new WithdrawRequest();
    request.fromAccountId = this.getFormFieldValue(this.FieldName.fromAccountId);
    request.amount = +this.getFormFieldValue(this.FieldName.amount);
    return request;
  }

  private getFormFieldValue(fieldName: string): any {
    return this.formService.getControlValue(this.withdrawForm, fieldName);
  }

  private setFormFieldValue(fieldName: string, fieldValue: string): void {
    return this.formService.setControlValue(this.withdrawForm, fieldName, fieldValue);
  }

  private setDefaultFieldValues(): void {
    if (this.accounts && this.accounts.length !== 0) {
      const accountId = this.accounts[0].id;
      this.setFormFieldValue(this.FieldName.fromAccountId, accountId);
    }
  }
}
