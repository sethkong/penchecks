import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldHintComponent } from './field-hint.component';

describe('FieldHintComponent', () => {
  let component: FieldHintComponent;
  let fixture: ComponentFixture<FieldHintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldHintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldHintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
