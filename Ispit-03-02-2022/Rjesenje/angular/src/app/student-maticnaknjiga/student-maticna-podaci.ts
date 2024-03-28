export interface UpisaneGodineGetVM {
  id: number
  ime: string
  prezime: string
  upisaneAkGodine: UpisaneAkGodine[]
}

export interface UpisaneAkGodine {
  id: number
  datum4_LjetniOvjera: string
  datum3_LjetniUpis: string
  datum2_ZimskiOvjera: string
  datum1_ZimskiUpis: string
  godinaStudija: number
  akGodina: string
  cijenaSkolarine: number
  evidentiraoKorisnik: string
  obnovaGodine: boolean
}
export interface UpisZimskiVM{
  datumUpisa:Date,
  studentId:number,
  godinaStudija:number,
  akGodina:number,
  cijenaSkolarine:number,
  obnova:boolean,
  korisnikId:number
}
export interface AkGodineCmb{
  id:number,
  opis:string
}

