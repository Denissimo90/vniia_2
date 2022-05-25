import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetentionTeamDetailListComponent } from './competention-team-detail-list.component';

describe('CompetentionTeamDetailListComponent', () => {
  let component: CompetentionTeamDetailListComponent;
  let fixture: ComponentFixture<CompetentionTeamDetailListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetentionTeamDetailListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetentionTeamDetailListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
