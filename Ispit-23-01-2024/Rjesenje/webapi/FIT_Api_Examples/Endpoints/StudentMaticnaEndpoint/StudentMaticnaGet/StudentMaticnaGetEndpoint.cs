using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentMaticnaEndpoint.StudentMaticnaGet
{
    [Route("UpisaneGodine")]
    public class StudentMaticnaGetEndpoint : MyBaseEndpoint<StudentMaticnaGetRequest, StudentmaticnaGetResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentMaticnaGetEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Get")]
        public override async Task<StudentmaticnaGetResponse> Obradi([FromQuery] StudentMaticnaGetRequest request, CancellationToken cancellationToken)
        {
            var student = _context.Student.FirstOrDefault(x => x.id == request.StudentId);
            var godine = await _context.UpisaneGodine.Where(x => x.StudentId == request.StudentId).Select(x =>
            new StudentmaticnaGetResponseGodine
            {
                Id = x.Id,
                AkGodina = x.AkademskaGodina,
                AkGodinaId = x.AkademskaGodinaId,
                GodinaStudija = x.Godinastudina,
                Obnova = x.JelObnova,
                DatumUpisa = x.DatumUpisZimski,
                DatumOvjere = x.DatumOvjeraZimski,
                CijenaSkolarine = x.CijenaSkolarine,
                EvidentiraoKorisnikId = x.EvidentiraoKorisnikId,
                EvidentiraoKorisnik = x.EvidentiraoKorisnik
            }
            ).ToListAsync(cancellationToken: cancellationToken);

            return new StudentmaticnaGetResponse
            {

                StudentId = student.id,
                Ime = student.ime,
                Prezime = student.prezime,
                UpisaneGodine = godine

            };
        }
    }
}
