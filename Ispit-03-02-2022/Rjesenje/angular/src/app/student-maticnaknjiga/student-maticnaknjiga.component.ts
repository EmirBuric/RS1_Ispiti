import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {AkGodineCmb, UpisaneGodineGetVM, UpisZimskiVM} from "./student-maticna-podaci";
import {MojConfig} from "../moj-config";
import {AutentifikacijaToken} from "../_helpers/login-informacije";

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css'],
})
export class StudentMaticnaknjigaComponent implements OnInit {
  student:number;
  podaci:UpisaneGodineGetVM;
  upisiSemestar: UpisZimskiVM;
  korisnik: number;
  modalTitle='';
  akGodine:AkGodineCmb[];
  constructor(private  _http:HttpClient,private route:ActivatedRoute) {}

  ngOnInit() {
    this.student=this.route.snapshot.params['id']
    this.getPodaci(this.student)
    this.getKorisnik()
    this.getAkGodineCmb()
  }

  ovjeriLjetni() {}

  upisLjetni() {}

  ovjeriZimski(id:number) {
    let url= MojConfig.adresa_servera+`/MaticnaKnjiga/OvjeriZimski/${id}`
    this._http.post(url,null,MojConfig.http_opcije()).subscribe(x=>{
      porukaSuccess("Uspjesno ovjeren semestar")
      this.ngOnInit()
    })
  }

  getPodaci(id:number){
    let url= MojConfig.adresa_servera+`/MaticnaKnjiga/GetByStudent/${id}`
    this._http.get<UpisaneGodineGetVM>(url,MojConfig.http_opcije()).subscribe(x=>{
      this.podaci=x
      this.modalTitle=x.ime+' '+x.prezime
    })
  }
  getKorisnik(){
    let url= MojConfig.adresa_servera+`/Autentifikacija/Get`
    this._http.get<AutentifikacijaToken>(url,MojConfig.http_opcije()).subscribe(x=>{
      this.korisnik=x.korisnickiNalogId
    })
  }
  getAkGodineCmb(){
    let url= MojConfig.adresa_servera+`/AkademskeGodine/GetAll_ForCmb`
    this._http.get<AkGodineCmb[]>(url,MojConfig.http_opcije()).subscribe(x=>{
      this.akGodine=x
    })
  }
  upisiZimski() {
    this.upisiSemestar={
      datumUpisa:new Date(),
      studentId:this.student,
      godinaStudija:1,
      akGodina:1,
      cijenaSkolarine:0,
      obnova:false,
      korisnikId:this.korisnik
    }
  }

  zatvori() {
    this.upisiSemestar=null
  }

  spasi() {
    let url= MojConfig.adresa_servera+`/MaticnaKnjiga/UpisiZimski`
    this._http.put(url,this.upisiSemestar,MojConfig.http_opcije()).subscribe(x=>{
      porukaSuccess("Uspjesno upisan semestar")
      this.zatvori()
      this.ngOnInit()
    })
  }
}
