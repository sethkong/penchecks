import { BaseEntity } from './base-entity.model';
import { EntityKind } from './entity-kind.model';

export interface Account extends BaseEntity {
    name: string;
    accountNumber: string;
    routingNumber: string;
    balance: number;
    accountTypeId: string;
    accountType: EntityKind;
}

export interface AccountRequest {
    name: string;
    accountNumber: string;
    routingNumber: string;
    balance: number;
    accountTypeId: string;
}

export class TransferRequest {
    fromAccountId: string = '';
    toAccountId: string = '';
    amount: number = 0;
    transferAt?: Date;
    notes: string = '';
}

export class DepositRequest {
    toAccountId: string = '';
    amount: number = 0;
}

export class WithdrawRequest {
    fromAccountId: string = '';
    amount: number = 0;
}