import { Component, Input } from '@angular/core';

@Component({
  selector: 'kh-field-hint',
  templateUrl: './field-hint.component.html',
  styleUrls: ['./field-hint.component.scss']
})
export class FieldHintComponent {

  @Input() message: string = '';

  constructor() { }
}
