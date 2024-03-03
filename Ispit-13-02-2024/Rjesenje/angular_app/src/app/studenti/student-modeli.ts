export interface StudentPretragaResponse {
  student: StudentPretragaResponseStudent[]
}

export interface StudentPretragaResponseStudent {
  id: number
  ime: string
  prezime: string
  broj_indeksa: string
  opstina_rodjenja_id: number
  opstina_rodjenja: OpstinaRodjenja
  created_time: string
}

export interface OpstinaRodjenja {
  id: number
  description: string
  drzava_id: number
  drzava: Drzava
}

export interface Drzava {
  id: number
  naziv: string
}
export interface StudentPretragaRequest{
  imePrezime:string,
  opstina:string
}

export interface OpstinaCmb{
  id:number,
  opis:string
}
export interface StudentEditRequest{
  id:number,
  ime:string,
  prezime:string,
  opstinaId:number
}

export interface StudentEditResponse{
  id:number
}
export interface StudentObrisiRequest{
  id:number
}
