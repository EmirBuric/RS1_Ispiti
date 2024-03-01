using System;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoint.StudentMaticnaDodaj
{
    public class StudentMaticnaDodajRequest
    {
        public int StudentId { get; set; }
        public int AkademskaGodinaId { get; set; }

        public int GodinaStudija { get; set; }
        public bool Obnova { get; set; }
        public DateTime DatumUpisa{ get; set; }
        public float CijenaSkolarine { get; set; }
        public int EvidentiraoKorisnikId { get; set; }
    }
}
