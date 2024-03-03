using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Endpoints.OpstinaEndponts.GetDefaultOpstina;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoints.StudentMaticnaGet
{
    [Route("UpisaneGodine")]
    public class StudentMaticnaGetEndpoint : MyBaseEndpoint<StudentMaticnaGetRequest, StudentMaticnaGetResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentMaticnaGetEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Get")]
        public override async Task<StudentMaticnaGetResponse> Obradi([FromQuery]StudentMaticnaGetRequest request, CancellationToken cancellationToken)
        {
            
            var student=await _context.Student.FindAsync(request.StudentId);
            if (student == null)
            {
                throw new Exception("Ne postoji student sa Id" + request.StudentId);
            }
            var godine = new StudentMaticnaGetResponse
            {
                StudentId = student.id,
                Ime = student.ime,
                Prezime = student.prezime,
                UpisaneGodine = await _context.UpisaneGodine.Where(x => x.StudentId == request.StudentId).Select(
                    x => new StudentMaticnaGetResponseGodine
                    {
                        GodinaId = x.GodinaId,
                        DatumOvjere = x.DatumOvjere,
                        DatumUpisa = x.DatumUpisa,
                        Obnova = x.Obnova,
                        GodinaStudija= x.GodinaStudija,
                        AkademskaGodina = x.AkGodina.opis,
                        EvidentiraoKorisnik = x.EvidentiraoKorisnik.korisnickoIme
                    }).ToListAsync(cancellationToken: cancellationToken)
            };
            return godine;
        }
    }
}
