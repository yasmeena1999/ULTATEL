import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../Models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'https://yourapi.com/api/students';

  constructor(private http: HttpClient) { }

  getStudents(pageIndex: number, pageSize: number): Observable<any> {
      let params = new HttpParams();
      params = params.append('pageIndex', pageIndex.toString());
      params = params.append('pageSize', pageSize.toString());
      return this.http.get(this.apiUrl, { params });
  }

  getStudent(id: number): Observable<Student> {
      return this.http.get<Student>(`${this.apiUrl}/${id}`);
  }

  addStudent(student: Student): Observable<Student> {
      return this.http.post<Student>(this.apiUrl, student);
  }

  updateStudent(id: number, student: Student): Observable<void> {
      return this.http.put<void>(`${this.apiUrl}/${id}`, student);
  }

  deleteStudent(id: number): Observable<void> {
      return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  searchStudents(query: any): Observable<Student[]> {
      return this.http.post<Student[]>(`${this.apiUrl}/search`, query);
  }
}
