import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {
  AkGodineCmb,
  StudentMaticnaDodajRequest,
  StudentMaticnaGetResponse,
  StudentMaticnaOvjeriRequest, StudentMaticnaOvjeriResponse
} from "./student-maticnaknjiga-modeli";
import {AutentifikacijaToken} from "../_helpers/login-informacije";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {

  studentId:number;
  noviSemestar: StudentMaticnaDodajRequest;
  response:StudentMaticnaGetResponse| null=null;
  modalTitle:string;
  korisnikId: number;
  akGodine:AkGodineCmb[];
  ovjeriSemestar:StudentMaticnaOvjeriRequest;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {}

  ovjeriLjetni(s:any) {

  }

  upisLjetni(s:any) {

  }

  ovjeriZimski(s:any) {

  }

  ngOnInit(): void {
    this.studentId=this.route.snapshot.params['id'];
    this.getUpisaneGodine(this.studentId);
    this.getAkGodine();
    this.getTrenutniKorisnik();
  }
  getUpisaneGodine(id:number){
    let url=MojConfig.adresa_servera+`/UpisaneGodine/Get?StudentId=${id}`;
    this.httpKlijent.get<StudentMaticnaGetResponse>(url,MojConfig.http_opcije()).subscribe(x=>{
      if(x && x.ime && x.prezime){
        this.response=x
        this.modalTitle=x.ime+' '+x.prezime
      }
    })
  }
  getTrenutniKorisnik(){
    let url=MojConfig.adresa_servera+'/Autentifikacija/Get';
    this.httpKlijent.get<AutentifikacijaToken>(url,MojConfig.http_opcije()).subscribe(x=>
    {
      if (x && x.korisnickiNalogId) {
        this.korisnikId = x.korisnickiNalogId
      }
    })
  }
  ovjeri(id:number){
    this.ovjeriSemestar={
      godinaId:id,
      datumOvjere:new Date(),
      napomena:''
    }
  }
  getAkGodine(){
    let url=MojConfig.adresa_servera+'/AkademskeGodine/GetAll_ForCmb';
    this.httpKlijent.get<AkGodineCmb[]>(url,MojConfig.http_opcije()).subscribe(x=>{
      if(x){
        this.akGodine=x
      }
    })
  }
  otvori() {
    this.noviSemestar={
      studentId:this.studentId,
      datumUpisa:new Date(),
      godinaStudija:1,
      akGodina:0,
      cijenaSkolarine:0,
      obnova:false,
      evidentiraoKorisnik:this.korisnikId
    }
    console.log(this.noviSemestar)
  }

  zatvori() {
    this.noviSemestar=null;
  }
  spremi(){
    let url=MojConfig.adresa_servera+'/UpisaneGodine/Dodaj'
    this.httpKlijent.put<void>(url,this.noviSemestar,MojConfig.http_opcije()).subscribe(x=>{
      porukaSuccess("Uspjesno dodan semestar");
      this.ngOnInit();
      this.zatvori();
    })
  }

  zatvoriOvjeru() {
    this.ovjeriSemestar=null;
  }

  spremiOvjeru() {
    let url=MojConfig.adresa_servera+'/UpisaneGodine/Ovjeri'
    this.httpKlijent.post<StudentMaticnaOvjeriResponse>(url,this.ovjeriSemestar,MojConfig.http_opcije()).subscribe(x=>{
      if(x){
        porukaSuccess("Uspjesno ovjeren semestar "+x.godinaId);
        this.ngOnInit();
        this.zatvoriOvjeru();
      }
      else {
        porukaError("Pojavila se greška pri ovjeri semestra "+x.godinaId)
      }
    })
  }
}
