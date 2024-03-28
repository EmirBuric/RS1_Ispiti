import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {StudentAddEditVM} from "../student-modeli";
import {FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";

@Component({
  selector: 'app-novi-student',
  templateUrl: './novi-student.component.html',
  styleUrls: ['./novi-student.component.css']
})
export class NoviStudentComponent implements OnInit {

  @Input()studentGet:StudentAddEditVM;
  form:FormGroup;
  @Output()dodaj= new EventEmitter<{
    student:StudentAddEditVM;
    id:number
  }>();
  @Output() closeModal=new EventEmitter<void>();
  opstineCmb:{id:number, opis:string}[]

  constructor(private _http:HttpClient) {}
  ngOnInit(): void {
    this.getOpstine();
  }
  getOpstine(){
    let url=MojConfig.adresa_servera+'/Opstina/GetByAll'
    this._http.get<{id:number, opis:string}[]>(url,MojConfig.http_opcije()).subscribe(x=>{
      this.opstineCmb=x
    })
  }

  zatvori() {
    this.closeModal.emit();
  }

  spasi() {
    const x={student:this.studentGet,id:this.studentGet.id}
    this.dodaj.emit(x);
  }

}
