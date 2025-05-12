import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { OverviewComponent } from './overview/overview.component';
import { TransferComponent } from './transfer/transfer.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
    OverviewComponent,
    TransferComponent,
    DepositComponent,
    WithdrawComponent,
  ],
  exports: [
    OverviewComponent,
    TransferComponent,
    DepositComponent,
    WithdrawComponent,
  ]
})
export class AccountsModule { }
