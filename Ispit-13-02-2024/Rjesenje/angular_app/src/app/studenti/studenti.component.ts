import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {
  OpstinaCmb,
  StudentEditRequest,
  StudentEditResponse, StudentObrisiRequest,
  StudentPretragaResponse,
  StudentPretragaResponseStudent
} from "./student-modeli";
import {AutentifikacijaToken} from "../_helpers/login-informacije";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: StudentPretragaResponseStudent[]=[];
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  editStudent: StudentEditRequest;
  opstine:OpstinaCmb[]=[];
  noviStudent: StudentEditRequest;
  defaultOpstina:number;
  zaBrisanje:StudentObrisiRequest;
  korisnikId: number;


  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  fetchStudenti() :void
  {
    this.httpKlijent.get<StudentPretragaResponse>(MojConfig.adresa_servera+ "/Student/Pretraga", MojConfig.http_opcije()).subscribe(x=>{
      this.studentPodaci = x.student
    });
  }
pretraga($event:Event)
{
  let url=MojConfig.adresa_servera+`/Student/Pretraga?ImePrezime=${this.ime_prezime}&Opstina=${this.opstina}`
  this.httpKlijent.get<StudentPretragaResponse>(url,MojConfig.http_opcije()).subscribe(x=>{
    if(x){
      this.studentPodaci=x.student
    }
  })
}
getOpstine(){
    let url= MojConfig.adresa_servera+ '/Opstina/GetByAll'
    this.httpKlijent.get<OpstinaCmb[]>(url,MojConfig.http_opcije()).subscribe(x=>{
      this.opstine=x
    })
}

edit(sId:number,sIme:string,sPrezime:string,sOpstinaId:number){
    this.editStudent={
      id:sId,
      ime:sIme,
      prezime:sPrezime,
      opstinaId:sOpstinaId
  }
}
zatvori(){
    this.editStudent=null;
}
spasiEdit(){
    let url=MojConfig.adresa_servera+'/Student/Edit';
    this.httpKlijent.post<StudentEditResponse>(url,this.editStudent,MojConfig.http_opcije()).subscribe(x=>{
      if(x){
        porukaSuccess("Uspjesno editovan student "+x.id)
        this.zatvori();
        this.ngOnInit();
      }
      else {
        porukaError("Editovanje Stidenta "+this.editStudent.id+" nije uspjelo")
      }
    })
}
getDefaultOpstina(){
  let url= MojConfig.adresa_servera+ '/Opstina/GetDefault'
  this.httpKlijent.get(url,MojConfig.http_opcije()).subscribe(x=>{
    //@ts-ignore
    this.defaultOpstina=x.id
  })
}
  ngOnInit(): void {
    this.fetchStudenti();
    this.getOpstine();
    this.getDefaultOpstina();
  }

  spasiNovi() {
    let url=MojConfig.adresa_servera+'/Student/Edit';
    this.httpKlijent.post<StudentEditResponse>(url,this.noviStudent,MojConfig.http_opcije()).subscribe(x=>{
      if(x){
        porukaSuccess("Uspjesno dodan student "+x.id)
        this.zatvoriNovi();
        this.ngOnInit();
      }
      else {
        porukaError("Dodavanje Studenta nije uspjelo")
      }
    })
  }
  novi(nId:number,nIme:string,nPrezime:string,nOpstina:number) {
    this.noviStudent={
      id:nId,
      ime:nIme,
      prezime:nPrezime,
      opstinaId:nOpstina
    }
  }
  zatvoriNovi(){
    this.noviStudent=null;
  }
  obrisiStudenta(id:number){
    this.zaBrisanje={
      id:id
    }
    let url=MojConfig.adresa_servera+`/Student/Obrisi`
    this.httpKlijent.post(url,this.zaBrisanje,MojConfig.http_opcije()).subscribe(x=>{
      if(x){
        porukaSuccess("Uspjesno obrisan student Id: "+id)
        this.ngOnInit();
      }
      else {
        porukaError("Niste uspjeli obrisati studenta Id: "+id)
      }
    })
  }
}
