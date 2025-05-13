import { Component, OnInit, ViewChild } from '@angular/core';
import { AccountService } from '../account.service';
import { Transaction } from '../models/transaction.model';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-transactions',
  standalone: false,
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss'
})
export class TransactionsComponent implements OnInit {
  transactions?: Transaction[] = [];
  dataSource = new MatTableDataSource<Transaction>();
  displayedColumns: string[] = ['postingDate', 'description', 'amount', 'balance', 'reconcile'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private accountService: AccountService,
    private route: ActivatedRoute) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    const params = this.route.snapshot.queryParams;
    const accountId = params['accountId'];
    console.log('accountId: ', accountId);
    this.accountService.getTransactions(accountId)
      .subscribe(response => {
        this.transactions = response.data;
        console.log('Transactions: ', this.transactions);
        this.dataSource = new MatTableDataSource<Transaction>(this.transactions);
      });
  }
}
