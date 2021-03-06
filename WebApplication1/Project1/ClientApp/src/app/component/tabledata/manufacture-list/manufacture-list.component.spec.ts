import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {ManufactureListComponent} from './manufacture-list.component';

describe('ManufactureListComponent', () => {
  let component: ManufactureListComponent;
  let fixture: ComponentFixture<ManufactureListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManufactureListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManufactureListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
