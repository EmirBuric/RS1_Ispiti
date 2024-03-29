﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Modul3.Models
{
    public class Ispit
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        
        [ForeignKey(nameof(PredmetID))] 
        public Predmet predmet { get; set; }
        public int PredmetID { get; set; }
        
        public DateTime DatumIspita { get; set; }

    }
}
