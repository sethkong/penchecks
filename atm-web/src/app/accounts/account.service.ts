import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { HttpService } from '../api/http.service';
import { ApiResponse } from '../api/http-response.model';
import { Account, AccountRequest, TransferRequest } from './models/account.model';
import { Transaction } from './models/transaction.model';
import { EntityKind } from './models/entity-kind.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends HttpService {

  getAccountsResource: string = 'GetAccounts';
  getTransactionsResource: string = 'GetTransactions';
  getTypesResource: string = 'GetTypes';
  depositResource: string = 'Deposit';
  withdrawResource: string = 'Withdraw';
  openAccountResource: string = 'OpenAccount';
  transferResource: string = 'Transfer';

  constructor(private http: HttpClient) {
    super();
  }

  getAccounts(): Observable<ApiResponse<Account[]>> {
    return this.http.get<ApiResponse<Account>>(this.getEndpoint(this.getAccountsResource),
      { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }

  getTransactions(accountId: string): Observable<ApiResponse<Transaction[]>> {
    let endpoint = this.getEndpoint(this.getTransactionsResource);
    endpoint = `${endpoint}?accountId=${accountId}`;
    return this.http.get<ApiResponse<Transaction[]>>(endpoint,
      { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }

  getTypes(code: string): Observable<ApiResponse<EntityKind[]>> {
    return this.http.get<ApiResponse<EntityKind[]>>(this.getEndpoint(this.getTypesResource),
      { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }

  deposit(accountId: string, amount: number): Observable<ApiResponse<Transaction>> {
    let endpoint = this.getEndpoint(this.depositResource);
    endpoint = `${endpoint}?accountId=${accountId}&amount=${amount}`;
    return this.http.post<ApiResponse<Transaction>>(endpoint,
      { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }

  withdraw(accountId: string, amount: number): Observable<ApiResponse<Transaction>> {
    let endpoint = this.getEndpoint(this.withdrawResource);
    endpoint = `${endpoint}?accountId=${accountId}&amount=${amount}`;
    return this.http.post<ApiResponse<Transaction>>(endpoint,
      { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }

  openAccount(request: AccountRequest): Observable<ApiResponse<Account>> {
    return this.http.post<ApiResponse<Transaction>>(this.getEndpoint(this.openAccountResource),
      request, { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }

  transfer(request: TransferRequest): Observable<ApiResponse<Transaction>> {
    return this.http.post<ApiResponse<Transaction>>(this.getEndpoint(this.transferResource),
      request, { headers: this.getCommonHttpHeaders() })
      .pipe(catchError(this.handleError));
  }
}
