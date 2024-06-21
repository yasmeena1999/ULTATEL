import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';



import { StudentService } from '../../../Services/StudentService/student.service';
import Swal from 'sweetalert2';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';




@Component({
  selector: 'app-student-modal',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './student-modal.component.html',
  styleUrl: './student-modal.component.css'
})
export class StudentModalComponent {
  @Input() student: any = {};
  @Output() saveEvent = new EventEmitter<any>();
  empForm: FormGroup;
 

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    public activeModal: NgbActiveModal,
   
  ) {
    this.empForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dob: ['',Validators.required],
      gender: ['',Validators.required],
      country: ['',Validators.required],
    });
    
  }

  ngOnInit(): void {
    if (this.student) {
      this.empForm.patchValue(this.student);
    }
    
  }
  

  onFormSubmit(): void {
    if (this.empForm.valid) {
      if (this.student) {
        this.studentService.editStudent(this.student.id, this.empForm.value).subscribe({
          next: (val: any) => {
            Swal.fire({
              icon: 'success',
              title: 'Success!',
              text: 'Student details updated!',
            }).then((result) => {
              if (result.isConfirmed) {
                this.closeModal();
              }
            });
          },
          error: (err: any) => {
            console.error(err);
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Failed to update student details!',
            });
          },
        });
      } else {
        this.studentService.addStudent(this.empForm.value).subscribe({
          next: (val: any) => {
            Swal.fire({
              icon: 'success',
              title: 'Success!',
              text: 'Student added successfully!',
            }).then((result) => {
              if (result.isConfirmed) {
               this.closeModal();
              }
            });
          },
          error: (err: any) => {
            console.error(err);
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Failed to add student!',
            });
          },
        });
      }
    }
  }
  closeModal() {
    this.activeModal.dismiss('Cross click');
  }
}