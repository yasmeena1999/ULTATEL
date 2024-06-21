import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentModalDeleteComponent } from './student-modal-delete.component';

describe('StudentModalDeleteComponent', () => {
  let component: StudentModalDeleteComponent;
  let fixture: ComponentFixture<StudentModalDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentModalDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentModalDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
