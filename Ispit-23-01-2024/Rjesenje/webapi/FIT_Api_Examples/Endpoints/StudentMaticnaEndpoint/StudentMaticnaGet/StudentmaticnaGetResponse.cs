using FIT_Api_Examples.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoint.StudentMaticnaGet
{
    public class StudentmaticnaGetResponse
    {
        public int StudentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public List<StudentmaticnaGetResponseGodine> UpisaneGodine { get; set; }
    }

    public class StudentmaticnaGetResponseGodine
    {
        public int Id { get; set; }
        [ForeignKey(nameof(AkGodina))]
        public int AkGodinaId { get; set; }
        public AkademskaGodina AkGodina { get; set; }
        public int GodinaStudija { get; set; }
        public bool Obnova { get; set; }
        public DateTime DatumUpisa { get; set; }
        public DateTime? DatumOvjere { get; set; }
        public float CijenaSkolarine { get; set; }
        [ForeignKey(nameof(EvidentiraoKorisnik))]
        public int EvidentiraoKorisnikId { get; set; }
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }

    }
}


