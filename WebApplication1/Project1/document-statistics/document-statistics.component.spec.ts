import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentStatisticsComponent } from './document-statistics.component';

describe('DocumentStatisticsComponent', () => {
  let component: DocumentStatisticsComponent;
  let fixture: ComponentFixture<DocumentStatisticsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DocumentStatisticsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
