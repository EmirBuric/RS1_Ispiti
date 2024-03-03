using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoints.StudentMaticnaDodaj
{
    [Route("UpisaneGodine")]
    public class StudentMaticnaDodajEndpoint : MyBaseEndpoint<StudentMaticnaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentMaticnaDodajEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPut("Dodaj")]
        public override async Task<NoResponse> Obradi(StudentMaticnaDodajRequest request, CancellationToken cancellationToken)
        {
            if(_context.UpisaneGodine.Any(x=>x.GodinaStudija==request.GodinaStudija
                && x.StudentId==request.StudentId
                && request.Obnova==false)) 
            {
                throw new Exception("Ne mozete dodati dva puta istu godinu bez obnove");
            }
            UpisaneGodine godine= new UpisaneGodine();

            godine.StudentId=request.StudentId;
            godine.DatumUpisa=request.DatumUpisa;
            godine.GodinaStudija=request.GodinaStudija;
            godine.AkGodinaId = request.AkGodina;
            godine.CijenaSkolarine=request.CijenaSkolarine;
            godine.Obnova=request.Obnova;
            godine.EvidentiraoKorisnikId = request.EvidentiraoKorisnik;

            await _context.AddAsync(godine,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken:cancellationToken);
            return new NoResponse();

        }
    }
}
