using System;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoint.StudentMaticnaOvjeri
{
    public class StudentMaticnaOvjeriRequest
    {
        public int UpisanaGodinaId { get; set; }
        public DateTime DatumOvjere { get; set; }
        public string Napomena { get; set; }
    }
}
