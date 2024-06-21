import { Component, OnInit } from '@angular/core';
import AccountService from '../../../Services/AccountService/account.service';
import { Student } from '../../../Models/student';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { StudentService } from '../../../Services/StudentService/student.service';
import Swal from 'sweetalert2';
import { StudentModalComponent } from '../student-modal/student-modal.component';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {

  studentForm!: FormGroup;
  allStudents: Student[] = []; // Assuming you have a Student interface or class defined
  displayedStudents: Student[] = []; 
 

  constructor(private fb: FormBuilder,private accountService: AccountService,private studentService:StudentService,private modelservice:NgbModal)  {

  }

  ngOnInit(): void {
    this.studentForm = this.fb.group({
      selectedName: [''],
      selectedCountry: [''],
      selectedMinAge: [null, Validators.min(0)],
      selectedMaxAge: [null, Validators.min(0)],
      selectedGender: ['']
    });
    this.getAllStudents();
  }

  searchStudents(): void {
    if (this.studentForm.valid) {
      const searchData = this.studentForm.value;
      // Implement search logic using this.studentService
      console.log(searchData);
    }
  }

  logout(): void {
    this.accountService.logout();
  }

  openAddEditModal(isNew: boolean): void {
    const modalRef = this.modelservice.open(StudentModalComponent);
    modalRef.componentInstance.isNew = isNew; // Example of passing data to modal
    modalRef.result.then((result) => {
      if (result === true) {
        console.log('Operation successful');
        this.getAllStudents(); // Refresh list after modal operation
      }
    }).catch((error) => {
      console.error('Modal error:', error);
    });
  }

  resetForm(): void {
    this.studentForm.reset();
    this.displayedStudents = [...this.allStudents]; // Reset displayed list
  }

  getAllStudents(): void {
    this.studentService.getAllStudents().subscribe({
      next: (students: Student[]) => {
        this.allStudents = students;
        this.displayedStudents = [...this.allStudents];
      },
      error: (error: any) => {
        console.error('Error fetching students:', error);
      }
    });
  }

  delete(studentId: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this action!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Delete'
    }).then((result) => {
      if (result.isConfirmed) {
        this.studentService.deleteStudent(studentId).subscribe({
          next: () => {
            Swal.fire('Deleted!', 'Student has been deleted.', 'success');
            this.getAllStudents(); // Refresh list after delete
          },
          error: (err: any) => {
            console.error('Delete error:', err);
            Swal.fire('Error', 'Failed to delete student.', 'error');
          }
        });
      }
    });
  }
}