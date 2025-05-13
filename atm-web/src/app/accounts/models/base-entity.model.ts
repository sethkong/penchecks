export interface BaseEntity {
    id: string;
    updatedAt?: Date;
    insertedAt?: Date;
    inactiveAt?: Date;
    insertedBy?: string;
    updatedBy?: string;
    deletedAt?: Date;
}