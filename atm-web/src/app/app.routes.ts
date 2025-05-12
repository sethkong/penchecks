import { Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { NotfoundComponent } from './home/notfound/notfound.component';
import { OverviewComponent } from './accounts/overview/overview.component';
import { TransferComponent } from './accounts/transfer/transfer.component';
import { DepositComponent } from './accounts/deposit/deposit.component';
import { WithdrawComponent } from './accounts/withdraw/withdraw.component';


export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'accounts', component: OverviewComponent },
    { path: 'transfer', component: TransferComponent },
    { path: 'deposit', component: DepositComponent },
    { path: 'withdraw', component: WithdrawComponent },
    { path: '**', component: NotfoundComponent },
];
