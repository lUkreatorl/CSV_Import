import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonService } from '../services/person.service';
import { Person } from '../models/person';

@Component({
  selector: 'app-person-table',
  templateUrl: './person-table.component.html',
  styleUrls: ['./person-table.component.css']
})
export class PersonTableComponent implements OnInit {
  persons: Person[] = [];
  personForms: FormGroup[] = [];

  constructor(private fb: FormBuilder, private personService: PersonService) { }

  ngOnInit() {
    this.loadPersons();
  }

  loadPersons() {
    this.personService.getPersons().subscribe((data: Person[]) => {
      this.persons = data;
      this.initializeForms();
    });
  }

  initializeForms() {
    this.personForms = this.persons.map(person =>
      this.fb.group({
        name: [person.name, Validators.required],
        dateOfBirth: [person.dateOfBirth, Validators.required],
        married: [person.married],
        phone: [person.phone, Validators.required],
        salary: [person.salary, [Validators.required, Validators.min(0)]]
      })
    );
  }

  savePerson(index: number) {
    if (this.personForms[index].valid) {
      const updatedPerson = { ...this.persons[index], ...this.personForms[index].value };
      this.personService.updatePerson(updatedPerson.id, updatedPerson).subscribe(() => {
        alert('Person updated successfully');
      });
    }
  }

  deletePerson(id: number) {
    this.personService.deletePerson(id).subscribe(() => {
      this.loadPersons();
    });
  }
}
