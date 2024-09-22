import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from '../models/person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl = 'https://localhost:7257/CSV';

  constructor(private http: HttpClient) { }

  getPersons(): Observable<Person[]> {
    return this.http.get<Person[]>(this.apiUrl);
  }

  updatePerson(id: number, person: Person): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, person);
  }

  deletePerson(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  uploadCsv(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    const headers = new HttpHeaders({
      'Accept': 'application/json'
    });

    return this.http.post(`${this.apiUrl}/upload`, formData, { headers });
  }
}
