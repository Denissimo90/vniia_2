import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkplaceSelectorComponent } from './workplace-selector.component';

describe('WorkplaceSelectorComponent', () => {
  let component: WorkplaceSelectorComponent;
  let fixture: ComponentFixture<WorkplaceSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkplaceSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkplaceSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
