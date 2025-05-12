import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FieldErrorComponent } from './field-error/field-error.component';
import { ErrorComponent } from './error/error.component';
import { FieldHintComponent } from './field-hint/field-hint.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule
    ],
    declarations: [
        FieldErrorComponent,
        FieldHintComponent,
        ErrorComponent
    ],
    exports: [
        FieldErrorComponent,
        FieldHintComponent,
        ErrorComponent
    ]
})
export class ErrorsModule { }
