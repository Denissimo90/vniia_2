import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {FinancialAidStatisticsComponent} from './financial-aid-statistics.component';

describe('FinancialAidStatisticsComponent', () => {
  let component: FinancialAidStatisticsComponent;
  let fixture: ComponentFixture<FinancialAidStatisticsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FinancialAidStatisticsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinancialAidStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
