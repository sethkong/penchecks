import { Account } from './account.model';
import { BaseEntity } from './base-entity.model';
import { EntityKind } from './entity-kind.model';

export interface Transaction extends BaseEntity {
    postingDate?: Date;
    transactionTypeId: string;
    transactionType: EntityKind;
    amount: number;
    balance: number;
    accountId?: string;
    account: Account;
    description: string;
}