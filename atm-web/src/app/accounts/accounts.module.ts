import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { OverviewComponent } from './overview/overview.component';
import { TransferComponent } from './transfer/transfer.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';
import { RouterModule } from '@angular/router';
import { TransactionsComponent } from './transactions/transactions.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
  ],
  declarations: [
    OverviewComponent,
    TransferComponent,
    DepositComponent,
    WithdrawComponent,
    TransactionsComponent,
  ],
  exports: [
    OverviewComponent,
    TransferComponent,
    DepositComponent,
    WithdrawComponent,
    TransactionsComponent,
  ],
})
export class AccountsModule { }
