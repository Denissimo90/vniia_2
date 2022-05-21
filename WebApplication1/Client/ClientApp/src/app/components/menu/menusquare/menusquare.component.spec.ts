import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MenusquareComponent } from './menusquare.component';

describe('MenusquareComponent', () => {
  let component: MenusquareComponent;
  let fixture: ComponentFixture<MenusquareComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MenusquareComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenusquareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
