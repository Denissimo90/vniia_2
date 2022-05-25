import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupWorkplaceRegistrationComponent } from './group-workplace-registration.component';

describe('GroupWorkplaceRegistrationComponent', () => {
  let component: GroupWorkplaceRegistrationComponent;
  let fixture: ComponentFixture<GroupWorkplaceRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupWorkplaceRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupWorkplaceRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
