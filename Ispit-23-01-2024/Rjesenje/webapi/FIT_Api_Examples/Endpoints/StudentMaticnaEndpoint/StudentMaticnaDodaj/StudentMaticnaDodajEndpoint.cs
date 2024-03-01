using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoint.StudentMaticnaDodaj
{
    [Microsoft.AspNetCore.Mvc.Route("UpisaneGodine")]
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
            if (_context.UpisaneGodine.Any(x => x.Godinastudina == request.GodinaStudija && x.StudentId == request.StudentId) &&
               request.Obnova == false)
            {
                throw new Exception("Ne mozete dodati dva puta istu godinu bez obnove!");
            }
                
            var upisanaGodina = new UpisaneGodine
            {
                StudentId= request.StudentId,
                AkademskaGodinaId = request.AkademskaGodinaId,
                CijenaSkolarine= request.CijenaSkolarine,
                Godinastudina= request.GodinaStudija,
                JelObnova= request.Obnova,
                DatumUpisZimski= request.DatumUpisa,
                EvidentiraoKorisnikId=request.EvidentiraoKorisnikId

            };
            await _context.AddAsync(upisanaGodina,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new NoResponse();
        }
    }
}
