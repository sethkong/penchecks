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

export interface TransferRequest {
    fromAccountId: string;
    toAccountId: string;
    account: number;
}