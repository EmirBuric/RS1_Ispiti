import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {
  StudentPretragaResponse,
  StudentPretragaResponseStudent,
  StudentEditRequest,
  StudentEditResponse, getAllOpstine, StudentSoftDeleteResponse, StudentSoftDeleteRequest
} from "./student-modeli";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
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
  editStudent:StudentEditRequest;
  newStudent:StudentEditRequest;
  opstine:getAllOpstine[]=[];
  defaultOpstina:number=1;
  zaBrisanje:StudentSoftDeleteRequest;
  defaultToken:AutentifikacijaToken;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi() :void
  {
    /*this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.studentPodaci = x;
    });*/
    this.httpKlijent.get<StudentPretragaResponse>(MojConfig.adresa_servera+'/Student/Pretrazi',MojConfig.http_opcije()).subscribe(x=>
      this.studentPodaci=x.studenti
    )

  }

  Pretraga($event: Event){
    let url= MojConfig.adresa_servera+ `/Student/Pretrazi?ime_prezime=${this.ime_prezime}&opstina_naziv=${this.opstina}`
    this.httpKlijent.get<StudentPretragaResponse>(url,MojConfig.http_opcije()).subscribe(x=>
    this.studentPodaci=x.studenti
    );
  }
  GetAllOpstine(){
    let url=MojConfig.adresa_servera+'/Opstina/GetByAll';
    this.httpKlijent.get<getAllOpstine[]>(url,MojConfig.http_opcije()).subscribe(x=>
      this.opstine=x
    )
  }

  UrediStudenta(sId:number,sIme:string,sPrezime:string,sOpstinaId:number){
    this.editStudent={
      id:sId,
      ime:sIme,
      prezime:sPrezime,
      opstinaId:sOpstinaId
    }
  }

  uredi(){

    let url=MojConfig.adresa_servera+ '/Student/Edit';
    this.httpKlijent.post<StudentEditResponse>(url,this.editStudent).subscribe(x=>{
      if(x!=null){
        porukaSuccess("Uspjesno ureden korisnik sa ID: "+ x.id)
      }
      else {
        porukaError("Korisnik se ne moze urediti")
      }
      this.ngOnInit();
      this.editStudent=null;
    })
  }
getDefaultOpstina(){
    let url=MojConfig.adresa_servera+'/Autentifikacija/Get';
    this.httpKlijent.get<AutentifikacijaToken>(url,MojConfig.http_opcije()).subscribe(x=>
    {
      if (x && x.korisnickiNalog && x.korisnickiNalog.defaultOpstinaId) {
        this.defaultOpstina = x.korisnickiNalog.defaultOpstinaId;
      }
    })
}
dodaj()
{
  let url=MojConfig.adresa_servera+ '/Student/Edit';
  this.httpKlijent.post<StudentEditResponse>(url,this.newStudent).subscribe(x=>{
    if(x!=null){
      porukaSuccess("Uspjesno dodan korisnik sa ID: "+ x.id)
    }
    else {
      porukaError("Korisnik se ne moze dodati")
    }
    this.ngOnInit();
    this.newStudent=null;
  })
}
dodajStudent(nId:number,nIme:string,nPrezime:string,nOpstinaId:number){
  this.newStudent={
    id:nId,
    ime:nIme,
    prezime:nPrezime,
    opstinaId:nOpstinaId
  }
}
  ngOnInit(): void {
    this.testirajWebApi();
    this.GetAllOpstine();
    this.getDefaultOpstina();
  }

  zatvori() {
    this.editStudent=null;
    this.newStudent=null;
  }
  Obrisi(id:number){
    this.zaBrisanje={
      id:id
    }
    let url= MojConfig.adresa_servera+'/Student/Obrisi';
    this.httpKlijent.post<StudentSoftDeleteResponse>(url,this.zaBrisanje).subscribe(x=>{
        if (x!=null){
          porukaSuccess("Uspjesno Obrisan student sa ID: "+x.id)
        }
        else {
          porukaError("Nije obrisan student sa ID: "+id)
        }
        this.ngOnInit();
      })
  }
}
