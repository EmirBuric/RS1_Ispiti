export interface StudentMaticnaGetRequest{
  studentId:number
}
export interface StudentmaticnaGetResponse{
  studentId:number,
  ime:string,
  prezime:string,
  upisaneGodine:StudentmaticnaGetResponseGodine[]
}
export interface StudentmaticnaGetResponseGodine{
  id: number
  akGodinaId: number
  akGodina: AkGodina
  godinaStudija: number
  obnova: boolean
  datumUpisa: string
  datumOvjere: string
  cijenaSkolarine: number
  evidentiraoKorisnikId: number
  evidentiraoKorisnik: EvidentiraoKorisnik
}

export interface AkGodina {
  id: number
  opis: string
}

export interface EvidentiraoKorisnik {
  id: number
  korisnickoIme: string
}

export interface StudentMaticanaDodajRequest{
  studentId:number,
  akademskaGodinaId:number,
  godinaStudija:number,
  obnova:boolean,
  datumUpisa:Date,
  cijenaSkolarine:number
  evidentiraoKorisnikId:number
}
export interface StudentMaticanaDodajResponse{
  godinaId:number
}

export interface OvjeraSemestraRequest{
  upisanaGodinaId:number,
  datumOvjere:Date,
  napomena:string
}

export interface OvjeraSemestraResponse{
  upisanaGodinaId:number
}
