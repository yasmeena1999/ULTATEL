import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../../Models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

    
        private apiUrl = '/api/students';
      
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
      
        deleteStudent(id: number): Observable<any> {
          return this.http.delete(`${this.apiUrl}/${id}`);
        }
      
        searchStudents(params: any): Observable<Student[]> {
          return this.http.get<Student[]>(`${this.apiUrl}/search`, { params });
        }
      }