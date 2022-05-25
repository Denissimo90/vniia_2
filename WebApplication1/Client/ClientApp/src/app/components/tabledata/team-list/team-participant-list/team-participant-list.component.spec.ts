import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamParticipantListComponent } from './team-participant-list.component';

describe('TeamParticipantListComponent', () => {
  let component: TeamParticipantListComponent;
  let fixture: ComponentFixture<TeamParticipantListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeamParticipantListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamParticipantListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
