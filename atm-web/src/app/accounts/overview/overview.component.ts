import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { Account } from '../models/account.model';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.scss',
})
export class OverviewComponent implements OnInit {

  accounts?: Account[] = [];

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.getAccounts().subscribe(response => {
      this.accounts = response.data;
      console.log('Accounts: ', this.accounts);
    });
  }
}
