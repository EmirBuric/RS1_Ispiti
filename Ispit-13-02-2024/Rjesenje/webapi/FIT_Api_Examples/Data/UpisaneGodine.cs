using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Data
{
    [Table("UpisaneGodine")]
    public class UpisaneGodine
    {
        [Key]
        public int GodinaId { get; set; }
        public int GodinaStudija { get; set; }
        public DateTime DatumUpisa { get; set; }
        [ForeignKey(nameof(AkGodinaId))]
        public AkademskaGodina AkGodina { get; set; }
        public int AkGodinaId { get; set; }
        public float CijenaSkolarine { get; set; }
        public bool Obnova { get; set; }
        public DateTime? DatumOvjere { get; set; }
        public string Napomena { get; set; }
        [ForeignKey(nameof(EvidentiraoKorisnikId))]
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }
        public int EvidentiraoKorisnikId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        public int StudentId { get; set; }


    }
}
