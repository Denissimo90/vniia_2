import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParticipantSelectorComponent } from './participant-selector.component';

describe('ParticipantSelectorComponent', () => {
  let component: ParticipantSelectorComponent;
  let fixture: ComponentFixture<ParticipantSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParticipantSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParticipantSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
