// import { Component, OnInit } from '@angular/core';
// import AccountService from '../../../Services/AccountService/account.service';
// import { Student } from '../../../Models/student';
// import { FormBuilder, FormGroup } from '@angular/forms';
// import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
// import { StudentService } from '../../../Services/StudentService/student.service';

// @Component({
//   selector: 'app-home',
//   standalone: true,
//   imports: [],
//   templateUrl: './home.component.html',
//   styleUrl: './home.component.css'
// })
// export class HomeComponent implements OnInit {
//   students: Student[] = [];
//   totalItems: number = 0;
//   pageSize: number = 10;
//   pageIndex: number = 1;
//   searchForm: FormGroup;

//   constructor(
//     private studentService: StudentService,
//     private fb: FormBuilder,
//     private modalService: NgbModal
//   ) {
//     this.searchForm = this.fb.group({
//       name: [''],
//       ageFrom: [''],
//       ageTo: [''],
//       country: [''],
//       gender: ['']
//     });
//   }

//   ngOnInit(): void {
//     this.loadStudents();
//   }

//   loadStudents(): void {
//     const filters = this.searchForm.value;
//     this.studentService.getStudents(this.pageIndex, this.pageSize, filters).subscribe(response => {
//       this.students = response.data;
//       this.totalItems = response.count;
//     });
//   }

//   onSearch(): void {
//     this.pageIndex = 1; // Reset to first page on new search
//     this.loadStudents();
//   }

//   onReset(): void {
//     this.searchForm.reset();
//     this.pageIndex = 1;
//     this.loadStudents();
//   }

//   onPageChange(page: number): void {
//     this.pageIndex = page;
//     this.loadStudents();
//   }

//   addStudent(): void {
//     const modalRef = this.modalService.open(StudentModalComponent);
//     modalRef.componentInstance.student = null;

//     modalRef.result.then((result) => {
//       if (result) {
//         this.studentService.addStudent(result).subscribe(() => {
//           this.loadStudents();
//         });
//       }
//     }, (reason) => {
//       // Handle dismiss reason if needed
//     });
//   }

//   editStudent(student: Student): void {
//     const modalRef = this.modalService.open(StudentModalComponent);
//     modalRef.componentInstance.student = student;

//     modalRef.result.then((result) => {
//       if (result) {
//         this.studentService.updateStudent(result).subscribe(() => {
//           this.loadStudents();
//         });
//       }
//     }, (reason) => {
//       // Handle dismiss reason if needed
//     });
//   }

//   deleteStudent(id: number): void {
//     const modalRef = this.modalService.open(DeleteConfirmationModalComponent);
//     modalRef.componentInstance.id = id;

//     modalRef.result.then((result) => {
//       if (result === 'confirm') {
//         this.studentService.deleteStudent(id).subscribe(() => {
//           this.loadStudents();
//         });
//       }
//     }, (reason) => {
//       // Handle dismiss reason if needed
//     });
//   }
// }

