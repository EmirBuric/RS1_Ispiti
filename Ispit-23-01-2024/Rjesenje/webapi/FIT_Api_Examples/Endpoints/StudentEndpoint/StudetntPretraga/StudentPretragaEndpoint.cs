using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FIT_Api_Examples.Endpoints.StudentEndpoint.StudetntPretraga
{
    [Microsoft.AspNetCore.Mvc.Route("Student")]
    public class StudentPretragaEndpoint : MyBaseEndpoint<StudentPretragaRequest, StudentPretragaResponse>
    {
        private readonly ApplicationDbContext _context;
        public StudentPretragaEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Pretrazi")]
        public override async Task<StudentPretragaResponse> Obradi([FromQuery]StudentPretragaRequest request, CancellationToken cancellationToken)
        {
            var student = await _context.Student.Include(x => x.opstina_rodjenja.drzava).Where(x =>
            (request.ime_prezime == null && request.opstina_naziv == null) ||
            (x.ime + " " + x.prezime).StartsWith(request.ime_prezime) ||
            (x.prezime + " " + x.ime).StartsWith(request.ime_prezime) ||
            x.opstina_rodjenja.description.StartsWith(request.opstina_naziv)
            ).Where(x=>x.Obrisan==false).Select(x => new StudentPretragaResponseStudent()
            {
                id = x.id,
                ime = x.ime,
                prezime = x.prezime,
                broj_indeksa = x.broj_indeksa,
                opstina_rodjenja_id = x.opstina_rodjenja_id,
                opstina_rodjenja = x.opstina_rodjenja,
                created_time = x.created_time,
            }).ToListAsync(cancellationToken: cancellationToken);
            return new StudentPretragaResponse { Studenti = student };
        }
    }
}
