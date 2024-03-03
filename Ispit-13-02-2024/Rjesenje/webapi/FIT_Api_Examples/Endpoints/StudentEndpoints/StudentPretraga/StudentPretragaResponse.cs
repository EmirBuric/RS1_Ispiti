using FIT_Api_Examples.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace FIT_Api_Examples.Endpoints.StudentEndpoints.StudentPretraga
{
    public class StudentPretragaResponse
    {
        public List<StudentPretragaResponseStudent> Student { get; set; }
    }

    public class StudentPretragaResponseStudent
    {
        public int Id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string broj_indeksa { get; set; }
        [ForeignKey(nameof(opstina_rodjenja))]
        public int? opstina_rodjenja_id { get; set; }
        public Opstina opstina_rodjenja { get; set; }
        public DateTime created_time { get; set; }
    }
}
