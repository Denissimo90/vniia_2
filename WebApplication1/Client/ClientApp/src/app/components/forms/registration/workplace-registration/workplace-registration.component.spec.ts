import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkplaceRegistrationComponent } from './workplace-registration.component';

describe('WorkplaceRegistrationComponent', () => {
  let component: WorkplaceRegistrationComponent;
  let fixture: ComponentFixture<WorkplaceRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkplaceRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkplaceRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
