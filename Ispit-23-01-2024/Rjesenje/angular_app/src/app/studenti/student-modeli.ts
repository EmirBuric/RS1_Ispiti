export interface StudentPretragaRequest{
  ime_prezime:string,
  opstina_naziv:string
}

export interface StudentPretragaResponse{
  studenti:StudentPretragaResponseStudent[];
}
export interface StudentPretragaResponseStudent{
  id:number,
  ime:string,
  prezime:string,
  broj_indeksa:string,
  opstina_rodjenja_id: number,
  opstina_rodjenja: Opstina,
  created_time:Date
}

export interface Opstina{
  id:number,
  description:string,
  drzava_id:number,
  drzava:Drzava,
}

export interface Drzava{
  id:number,
  naziv:string
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

export interface getAllOpstine{
  id:number,
  opis:string
}

export interface StudentSoftDeleteRequest{
  id:number
}
export interface StudentSoftDeleteResponse{
  id:number
}
