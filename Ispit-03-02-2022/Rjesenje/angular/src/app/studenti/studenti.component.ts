import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MojConfig } from '../moj-config';
import { Router } from '@angular/router';
import {Student, StudentAddEditVM} from "./student-modeli";
declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css'],
})
export class StudentiComponent implements OnInit {
  title: string = 'angularFIT2';
  ime: string = '';
  studentPodaci: Student[];
  editStudent:StudentAddEditVM;
  noviStudent:StudentAddEditVM;

  constructor(private httpKlijent: HttpClient, private router: Router) {}

  testirajWebApi(): void {
    this.httpKlijent
      .get<Student[]>(
        MojConfig.adresa_servera + '/Student/GetAll',
        MojConfig.http_opcije()
      )
      .subscribe((x: any) => {
        this.studentPodaci = x;
      });
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }

  Pretraga($event: Event) {
    this.httpKlijent
      .get<Student[]>(
        MojConfig.adresa_servera + `/Student/GetAll?ime_prezime=${this.ime}`,
        MojConfig.http_opcije()
      )
      .subscribe((x: any) => {
        this.studentPodaci = x;
      });
  }

  onEditStudent($event: { student: StudentAddEditVM; id: number }) {
      let url=MojConfig.adresa_servera+`/Student/Update/${$event.id}`
      this.httpKlijent.post(url,$event.student,MojConfig.http_opcije()).subscribe(x=>{
        porukaSuccess("Uspjesno editovan student "+x)
        this.editStudent=null
        this.ngOnInit();
      })
  }

  edit(id: number, ime: string, prezime: string, opstina_rodjenja_id: number) {
    this.editStudent={
      id:id,
      ime:ime,
      prezime:prezime,
      opstina_rodjenja_id:opstina_rodjenja_id
    }
  }

  onDodajStudent($event: { student: StudentAddEditVM; id: number }) {
    let url=MojConfig.adresa_servera+`/Student/Update/${$event.id}`
    this.httpKlijent.post(url,$event.student,MojConfig.http_opcije()).subscribe(x=>{
      porukaSuccess("Uspjesno dodan student "+x)
      this.noviStudent=null
      this.ngOnInit();
    })
  }

  dodaj() {
    this.noviStudent={
      id:0,
      ime:'',
      prezime:'',
      opstina_rodjenja_id:0
    }
  }
}
