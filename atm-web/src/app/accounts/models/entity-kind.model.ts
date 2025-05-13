import { BaseEntity } from './base-entity.model';

export interface EntityKind extends BaseEntity {
    name: string;
    code: string;
    description: string;
    parentId?: string;
}