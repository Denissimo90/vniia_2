import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetentListComponent } from './competent-list.component';

describe('CompetentListComponent', () => {
  let component: CompetentListComponent;
  let fixture: ComponentFixture<CompetentListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetentListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
