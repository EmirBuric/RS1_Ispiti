using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentEndpoints.StudentPretraga
{
    [Route("Student")]
    public class StudentPretragaEndpoint : MyBaseEndpoint<StudentPretragaRequest, StudentPretragaResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentPretragaEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Pretraga")]
        public override async Task<StudentPretragaResponse> Obradi([FromQuery]StudentPretragaRequest request, CancellationToken cancellationToken)
        {
            var student = await _context.Student.Include(x => x.opstina_rodjenja.drzava).Where(x =>
            (request.ImePrezime == null && request.Opstina == null) ||
            (x.ime + ' ' + x.prezime).StartsWith(request.ImePrezime) ||
            (x.prezime + ' ' + x.ime).StartsWith(request.ImePrezime) ||
            (x.opstina_rodjenja.drzava.naziv + " - " + x.opstina_rodjenja.description).StartsWith(request.Opstina)
            ).Where(x=>x.Obrisan==false).Select(x => new StudentPretragaResponseStudent
            {
                Id = x.id,
                ime = x.ime,
                prezime = x.prezime,
                broj_indeksa = x.broj_indeksa,
                opstina_rodjenja = x.opstina_rodjenja,
                opstina_rodjenja_id = x.opstina_rodjenja_id,
                created_time = x.created_time
            }).ToListAsync(cancellationToken);
            return new StudentPretragaResponse
            {
                Student = student
            };
        }
    }
}
