import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetentRegistrationComponent } from './competent-registration.component';

describe('CompetentRegistrationComponent', () => {
  let component: CompetentRegistrationComponent;
  let fixture: ComponentFixture<CompetentRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetentRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetentRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
