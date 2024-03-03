using System;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoints.StudentMaticnaOvjeri
{
    public class StudentMaticnaOvjeriRequest
    {
        public int GodinaId { get; set; }
        public DateTime DatumOvjere { get; set; }
        public string Napomena { get; set; }
    }
}
