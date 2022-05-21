import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {ManufactureRegistrationComponent} from './manufacture-registration.component';

describe('ManufactureRegistrationComponent', () => {
  let component: ManufactureRegistrationComponent;
  let fixture: ComponentFixture<ManufactureRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManufactureRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManufactureRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
