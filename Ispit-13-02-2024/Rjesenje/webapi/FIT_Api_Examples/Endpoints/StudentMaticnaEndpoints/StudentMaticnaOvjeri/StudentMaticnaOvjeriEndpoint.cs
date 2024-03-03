using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoints.StudentMaticnaOvjeri
{
    [Route("UpisaneGodine")]
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
            var ovjera = _context.UpisaneGodine.FirstOrDefault(x => x.GodinaId == request.GodinaId);
            if (ovjera == null)
            {
                throw new Exception("Ne postoji godina sa ovim ID " + request.GodinaId);
            }
            ovjera.DatumOvjere=request.DatumOvjere;
            ovjera.Napomena=request.Napomena;
            await _context.SaveChangesAsync(cancellationToken);
            return new StudentMaticnaOvjeriResponse { GodinaId=ovjera.GodinaId };
        }
    }
}
