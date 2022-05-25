import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupWorkplaceListComponent } from './group-workplace-list.component';

describe('GroupWorkplaceListComponent', () => {
  let component: GroupWorkplaceListComponent;
  let fixture: ComponentFixture<GroupWorkplaceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupWorkplaceListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupWorkplaceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
