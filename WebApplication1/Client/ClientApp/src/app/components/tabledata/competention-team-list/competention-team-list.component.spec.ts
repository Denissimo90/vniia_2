import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetentionTeamListComponent } from './competention-team-list.component';

describe('CompetentionTeamListComponent', () => {
  let component: CompetentionTeamListComponent;
  let fixture: ComponentFixture<CompetentionTeamListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetentionTeamListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetentionTeamListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
