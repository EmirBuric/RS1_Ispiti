import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { MojConfig } from '../../moj-config';
import {Student, StudentAddEditVM} from "../student-modeli";
import {FormBuilder, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(s: string): any;

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css'],
})
export class EditStudentComponent implements OnInit {
  @Input()studentGet:StudentAddEditVM;
  form:FormGroup;
  @Output()edit= new EventEmitter<{
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
    this.edit.emit(x);
  }
}
