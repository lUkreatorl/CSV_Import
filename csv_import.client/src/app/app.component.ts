import { Component, OnInit } from '@angular/core';
import { PersonService } from './services/person.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CSV Upload and Person Records';
  selectedFile: File | null = null;
  persons: any[] = [];

  constructor(private personService: PersonService) { }

  ngOnInit(): void {
    this.loadPersons();
  }

  loadPersons(): void {
    this.personService.getPersons().subscribe(
      (data: any[]) => {  
        this.persons = data;
      },
      (error: any) => { 
        console.error('Error fetching persons', error);
      }
    );
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }
  
  onUpload(): void {
    if (this.selectedFile) {
      this.personService.uploadCsv(this.selectedFile).subscribe(
        (response: any) => { 
          alert('File uploaded successfully!');
          this.loadPersons(); 
        },
        (error: any) => {  
          console.error('Error uploading file', error);
        }
      );
    } else {
      alert('Please select a file first.');
    }
  }
}
