import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetentParticipantListComponent } from './competent-participant-list.component';

describe('CompetentParticipantListComponent', () => {
  let component: CompetentParticipantListComponent;
  let fixture: ComponentFixture<CompetentParticipantListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetentParticipantListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetentParticipantListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
