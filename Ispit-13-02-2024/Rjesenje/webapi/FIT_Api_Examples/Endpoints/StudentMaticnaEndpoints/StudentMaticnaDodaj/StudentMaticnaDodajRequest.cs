using System;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoints.StudentMaticnaDodaj
{
    public class StudentMaticnaDodajRequest
    {
        public int StudentId { get; set; }
        public DateTime DatumUpisa { get; set; }
        public int GodinaStudija { get; set; }
        public int AkGodina { get; set; }
        public float CijenaSkolarine { get; set; }
        public bool Obnova { get; set; }
        public int EvidentiraoKorisnik { get; set; }
    }
}
