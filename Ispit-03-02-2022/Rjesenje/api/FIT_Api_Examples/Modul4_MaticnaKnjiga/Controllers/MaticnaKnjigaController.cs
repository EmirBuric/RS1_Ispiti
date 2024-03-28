using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3.Models;
using FIT_Api_Examples.Modul4_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public class UpisameGodineGetVM
        {
            public int id { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public List<UpisUAkGodinuPodaci> upisaneAkGodine { get; set; }
        }
        public class UpisUAkGodinuPodaci
        {
            public int id { get; set; }
            public DateTime? datum4_LjetniOvjera { get; set; }
            public DateTime? datum3_LjetniUpis { get; set; }
            public DateTime? datum2_ZimskiOvjera { get; set; }
            public DateTime? datum1_ZimskiUpis { get; set; }
            public int godinaStudija { get; set; }
            public bool obnovaGodine { get; set; }

            public string akGodina { get; set; }
            public string evidentiraoKorisnik { get; set; }

        }
        [HttpGet("{id}")]
        public ActionResult<UpisameGodineGetVM> GetByStudent(int id)
        {
            if(!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Nije logiran");
            var student= _dbContext.Student.FirstOrDefault(x=>x.id==id);
            if(student==null) return BadRequest("Nije nadjen student");

            var maticna = new UpisameGodineGetVM
            {
                id = student.id,
                ime = student.ime,
                prezime = student.prezime,
                upisaneAkGodine = _dbContext.UpisUAkGodinu.Where(x => x.studentId == id).Select(x=>new UpisUAkGodinuPodaci
                {
                    id= x.id,
                    datum1_ZimskiUpis=x.datum1_ZimskiUpis,
                    datum2_ZimskiOvjera=x.datum2_ZimskiOvjera,
                    datum3_LjetniUpis=x.datum3_LjetniUpis,
                    datum4_LjetniOvjera=x.datum4_LjetniOvjera,
                    godinaStudija=x.godinaStudija,
                    obnovaGodine=x.obnovaGodine,
                    akGodina=x.akademskaGodina.opis,
                    evidentiraoKorisnik=x.evidentiraoKorisnik.korisnickoIme

                }).ToList(),

            };
            return maticna;
        }
        public class UpisZimskiVM
        {
            public DateTime DatumUpisa { get; set; }
            public int StudentId { get; set; }
            public int GodinaStudija { get; set; }
            public int AkGodina { get; set; }
            public float? CijenaSkolarine { get; set; }
            public bool Obnova { get; set; }
            public int KorisnikId { get; set; }
        }
        [HttpPut]
        public ActionResult UpisiZimski(UpisZimskiVM zimski)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Nije logiran");
            var upis = new UpisUAkGodinu
            {
                datum1_ZimskiUpis = zimski.DatumUpisa,
                studentId = zimski.StudentId,
                godinaStudija = zimski.GodinaStudija,
                akademskaGodinaId = zimski.AkGodina,
                obnovaGodine = zimski.Obnova,
                cijenaSkolarine = zimski.CijenaSkolarine,
                evidentiraoKorisnikId = zimski.KorisnikId
            };
            if (_dbContext.UpisUAkGodinu.Any(x => x.studentId == upis.studentId && x.godinaStudija == upis.godinaStudija) && upis.obnovaGodine == false)
                return BadRequest("Nije obnova!");
            _dbContext.UpisUAkGodinu.Add(upis);
            return Ok(_dbContext.SaveChanges());
        }
        [HttpPost("{id}")]
        public IActionResult OvjeriZimski(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Nije logiran");

            var godina=_dbContext.UpisUAkGodinu.FirstOrDefault(x => x.id == id);
            godina.datum2_ZimskiOvjera=DateTime.Now;
            return Ok(_dbContext.SaveChanges());
        }
       

    }


}
