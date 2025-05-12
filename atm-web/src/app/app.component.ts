import { Component } from '@angular/core';
import { LayoutsModule } from './layouts/layouts.module';
import { HomeModule } from './home/home.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    LayoutsModule,
    HomeModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
}
