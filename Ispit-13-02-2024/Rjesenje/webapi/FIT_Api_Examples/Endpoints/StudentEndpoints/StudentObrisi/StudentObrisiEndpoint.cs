using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentEndpoints.StudentObrisi
{
    [Route("Student")]
    public class StudentObrisiEndpoint : MyBaseEndpoint<StudentObrisiRequest, StudentObrisiResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentObrisiEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("Obrisi")]
        public override async Task<StudentObrisiResponse> Obradi(StudentObrisiRequest request, CancellationToken cancellationToken)
        {
            var student=_context.Student.FirstOrDefault(x=>x.id==request.Id);
            if (student==null) {
                throw new Exception("Nema studenta za obrisati");
            }
            student.Obrisan = true;
            await _context.SaveChangesAsync(cancellationToken:cancellationToken);
            return new StudentObrisiResponse
            {
                Id = student.id
            };
        }
    }
}
