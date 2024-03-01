import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {
  AkGodina,
  StudentMaticanaDodajRequest,
  OvjeraSemestraRequest,
  StudentmaticnaGetResponse, OvjeraSemestraResponse
} from "./student-maticna-modeli";
import {AutentifikacijaToken} from "../_helpers/login-informacije";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;


@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }

  studentId:number;
  noviSemestar:StudentMaticanaDodajRequest| null=null;
  upisaneGodinePodaci:StudentmaticnaGetResponse;
  imePrezime:string;
  akademskeGodine:AkGodina[]=[];
  korisnikId:number;
  ovjeriSemestar:OvjeraSemestraRequest;
  ovjeriLjetni(s:any) {

  }

  upisLjetni(s:any) {

  }

  ovjeriZimski(s:any) {

  }

  getStudentUpisaneGodine(id:number){
    let url=MojConfig.adresa_servera+`/UpisaneGodine/Get?StudentId=${id}`
    this.httpKlijent.get<StudentmaticnaGetResponse>(url,MojConfig.http_opcije()).subscribe(x=>{
      if(x && x.ime && x.prezime)
      {
        this.upisaneGodinePodaci=x
        this.imePrezime= x.ime+" "+x.prezime
      }
    })
  }
  otvori(){
    this.noviSemestar={
      studentId:this.studentId,
      akademskaGodinaId:1,
      godinaStudija:1,
      obnova:false,
      datumUpisa:new Date(),
      cijenaSkolarine:0,
      evidentiraoKorisnikId:this.korisnikId

    }
  }

  zatvori(){
    this.noviSemestar=null;
  }
  ngOnInit(): void {
    this.studentId=this.route.snapshot.params['id'];
    this.getStudentUpisaneGodine(this.studentId);
    this.getAkGodine();
    this.getKorisnik();
  }

  getAkGodine(){
    let url= MojConfig.adresa_servera+'/AkademskeGodine/GetAll_ForCmb';
    this.httpKlijent.get<AkGodina[]>(url,MojConfig.http_opcije()).subscribe(x=>{
      if(x)
      {
        this.akademskeGodine=x
      }
    })
  }
  getKorisnik(){
    let url=MojConfig.adresa_servera+'/Autentifikacija/Get';
    this.httpKlijent.get<AutentifikacijaToken>(url,MojConfig.http_opcije()).subscribe(x=>
    {
      if (x && x.korisnickiNalogId) {
        this.korisnikId = x.korisnickiNalogId
      }
    })
  }
  snimi() {
    let url = MojConfig.adresa_servera + '/UpisaneGodine/Dodaj';
    this.httpKlijent.put<void>(url, this.noviSemestar).subscribe(x => {
      porukaSuccess('Uspjesno dodana godina ')
      this.ngOnInit();
      this.zatvori();
    })
  }
  otvoriOvjeru(godinaID:number){
    this.ovjeriSemestar={
      upisanaGodinaId:godinaID,
      datumOvjere:new Date(),
      napomena:''
    }
  }
  zatvoriOvjeru(){
    this.ovjeriSemestar=null;
  }
  ovjera(){
    let url=MojConfig.adresa_servera+'/UpisaneGodine/Ovjeri'
    this.httpKlijent.post<OvjeraSemestraResponse>(url,this.ovjeriSemestar,MojConfig.http_opcije()).subscribe(x=>{
      if(x){
        porukaSuccess("Uspjesno ovjeren zimski semestar za godinuId: "+x.upisanaGodinaId)
        this.zatvoriOvjeru()
        this.ngOnInit()
      }
      else{
        porukaError("Greska pri ovjeri zimskog semestra za godinuId:"+this.ovjeriSemestar.upisanaGodinaId)

      }
    })
  }



}
