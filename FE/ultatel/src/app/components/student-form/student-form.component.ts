import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../../Services/StudentService/student.service';
import { Student } from '../../Models/student';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-student-form',
    templateUrl: './student-form.component.html',
})
export class StudentFormComponent implements OnInit {
    studentForm!: FormGroup;
    studentId!: number;
    isEditMode: boolean = false;

    constructor(
        private fb: FormBuilder,
        private studentService: StudentService,
        private route: ActivatedRoute,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.studentId = this.route.snapshot.paramMap.get('id');
        this.isEditMode = !!this.studentId;

        this.studentForm = this.fb.group({
            name: ['', Validators.required],
            country: ['', Validators.required],
            gender: ['', Validators.required],
            age: ['', [Validators.required, Validators.min(1)]],
            createdDate: ['', Validators.required]
        });

        if (this.isEditMode) {
            this.studentService.getStudent(this.studentId).subscribe(
                (student: Student) => this.studentForm.patchValue(student),
                (error) => console.error('Error fetching student', error)
            );
        }
    }

    onSubmit(): void {
        if (this.studentForm.invalid) return;

        const student: Student = this.studentForm.value;

        if (this.isEditMode) {
            this.studentService.updateStudent(this.studentId, student).subscribe(
                () => {
                    Swal.fire('Success', 'Student updated successfully', 'success');
                    this.router.navigate(['/students']);
                },
                (error) => {
                    console.error('Error updating student', error);
                    Swal.fire('Error', 'Error updating student', 'error');
                }
            );
        } else {
            this.studentService.addStudent(student).subscribe(
                () => {
                    Swal.fire('Success', 'Student added successfully', 'success');
                    this.router.navigate(['/students']);
                },
                (error) => {
                    console.error('Error adding student', error);
                    Swal.fire('Error', 'Error adding student', 'error');
                }
            );
        }
    }
}
