using FIT_Api_Examples.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Endpoints.StudentEndpoint.StudetntPretraga
{
    public class StudentPretragaResponse
    {
        public List<StudentPretragaResponseStudent> Studenti { get; set; }
    }

    public class StudentPretragaResponseStudent
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string broj_indeksa { get; set; }
        [ForeignKey(nameof(opstina_rodjenja))]
        public int? opstina_rodjenja_id { get; set; }
        public Opstina opstina_rodjenja { get; set; }
        public DateTime created_time { get; set; }
    }
}
