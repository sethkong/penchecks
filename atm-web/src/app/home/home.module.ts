import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [
        HomeComponent,
        NotfoundComponent,
    ],
    exports: [
        HomeComponent,
        NotfoundComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
    ]
})
export class HomeModule { }
