import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
  MatError,
  MatFormField,
  MatFormFieldModule,
  MatLabel
} from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { ErrorsModule } from '../errors/errors.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatNativeDateModule } from '@angular/material/core';
import {MatDatepickerModule} from "@angular/material/datepicker";

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ErrorsModule,
    MatSlideToggleModule,
    MatBadgeModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatError,
    MatFormField,
    MatLabel,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatDividerModule,
    MatSidenavModule,
    MatMenuModule,
    MatPaginatorModule,
    MatTableModule,
    MatNativeDateModule,
    MatDatepickerModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ErrorsModule,
    MatSlideToggleModule,
    MatBadgeModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatError,
    MatFormField,
    MatLabel,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatDividerModule,
    MatSidenavModule,
    MatMenuModule,
    MatPaginatorModule,
    MatTableModule,
    MatNativeDateModule,
    MatDatepickerModule,
  ],
  providers: [
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'outline', floatLabel: 'always' }
    }
  ]
})
export class SharedModule { }
