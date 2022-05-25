import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetentWorkplaceListComponent } from './competent-workplace-list.component';

describe('CompetentWorkplaceListComponent', () => {
  let component: CompetentWorkplaceListComponent;
  let fixture: ComponentFixture<CompetentWorkplaceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetentWorkplaceListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetentWorkplaceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
