using FIT_Api_Examples.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoints.StudentMaticnaGet
{
    public class StudentMaticnaGetResponse
    {
        public int StudentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public List<StudentMaticnaGetResponseGodine> UpisaneGodine { get; set; }
    }

    public class StudentMaticnaGetResponseGodine
    {
        public int GodinaId { get; set; }
        public int GodinaStudija { get; set; }
        public DateTime DatumUpisa { get; set; }
        public string AkademskaGodina { get; set; }
        public bool Obnova { get; set; }
        public DateTime? DatumOvjere { get; set; }

        public string EvidentiraoKorisnik { get; set; }

    }
}
