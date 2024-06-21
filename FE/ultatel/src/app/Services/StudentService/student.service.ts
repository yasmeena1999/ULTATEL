import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../../Models/student';
import { StudentSearchDto } from '../../Models/StudentSearchDto';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

    
        private apiUrl = 'https://bfcc-102-40-186-20.ngrok-free.app/api/students';
      
        constructor(private http: HttpClient) { }
      
        getAllStudents(): Observable<Student[]> {
          return this.http.get<Student[]>(this.apiUrl);
        }
      
        getStudentById(id: number): Observable<Student> {
          return this.http.get<Student>(`${this.apiUrl}/${id}`);
        }
      
        addStudent(student: Student): Observable<Student> {
          return this.http.post<Student>(this.apiUrl, student);
        }
      
        editStudent(id: number, student: Student): Observable<any> {
          return this.http.patch(`${this.apiUrl}/${id}`, student);
        }
      
        deleteStudent(studentid: number): Observable<any> {
          return this.http.delete(`${this.apiUrl}/${studentid}`);
        }
      
        searchStudents(searchDto: StudentSearchDto): Observable<Student[]> {
          return this.http.post<Student[]>(`${this.apiUrl}/search`, searchDto);
        }
      }