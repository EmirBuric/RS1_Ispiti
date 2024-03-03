export interface StudentMaticnaGetResponse{
  studentId:number,
  ime:string,
  prezime:string,
  upisaneGodine: StudentMaticnaGetResponseGodine[]
}
export interface StudentMaticnaGetResponseGodine{
  godinaId:number,
  godinaStudija:number,
  datumUpisa:Date,
  akademskaGodina:string,
  obnova:boolean,
  datumOvjere:Date,
  evidentiraoKorisnik:string
}
export interface StudentMaticnaDodajRequest{
  studentId:number,
  datumUpisa:Date,
  godinaStudija:number,
  akGodina:number,
  cijenaSkolarine:number,
  obnova:boolean,
  evidentiraoKorisnik:number
}
export interface AkGodineCmb{
  id: number,
  opis: string
}
export interface StudentMaticnaOvjeriRequest{
  godinaId:number,
  datumOvjere:Date,
  napomena:string
}
export interface  StudentMaticnaOvjeriResponse{
  godinaId:number
}
