export interface Student {
  id:number,
  ime:string,
  prezime:string
  broj_indeksa:string,
  opstina_rodjenja_id:number,
  opstina_rodjenja:Opstina,
  created_time:Date,
  slika_korisnika:string
}
export interface Opstina{
  id:number,
  description:string,
  drzava_id:number,
  drzava:Drzava
}
export interface Drzava{
  id:number,
  naziv:string
}
export interface StudentAddEditVM{
  id:number,
  ime:string,
  prezime:string,
  opstina_rodjenja_id:number
}
