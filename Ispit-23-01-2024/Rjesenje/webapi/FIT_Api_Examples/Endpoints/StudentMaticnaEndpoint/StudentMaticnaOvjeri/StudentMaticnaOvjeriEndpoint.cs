using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoint.StudentMaticnaOvjeri
{
    [Microsoft.AspNetCore.Mvc.Route("UpisaneGodine")]
    public class StudentMaticnaOvjeriEndpoint : MyBaseEndpoint<StudentMaticnaOvjeriRequest, StudentMaticnaOvjeriResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentMaticnaOvjeriEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("Ovjeri")]
        public override async Task<StudentMaticnaOvjeriResponse> Obradi(StudentMaticnaOvjeriRequest request, CancellationToken cancellationToken)
        {
            var ovjera = _context.UpisaneGodine.FirstOrDefault(x => x.Id == request.UpisanaGodinaId);
            if (ovjera == null)
            {
                throw new Exception("Ne postoji korisnik sa ID: " + request.UpisanaGodinaId);
            }

            ovjera.DatumOvjeraZimski = request.DatumOvjere;
            ovjera.Napomena=request.Napomena;
            await _context.SaveChangesAsync(cancellationToken);

            return new StudentMaticnaOvjeriResponse
            {
                UpisanaGodinaId = ovjera.Id
            };
        }
    }
}
