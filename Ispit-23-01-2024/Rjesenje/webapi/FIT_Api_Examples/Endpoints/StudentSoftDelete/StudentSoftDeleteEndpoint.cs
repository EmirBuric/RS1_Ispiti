using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentSoftDelete
{
    [Microsoft.AspNetCore.Mvc.Route("Student")]
    public class StudentSoftDeleteEndpoint : MyBaseEndpoint<StudentSoftDeleterequest, StudentSoftDeleteResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentSoftDeleteEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("Obrisi")]
        public override async Task<StudentSoftDeleteResponse> Obradi(StudentSoftDeleterequest request, CancellationToken cancellationToken)
        {
            var student=_context.Student.FirstOrDefault(x=> x.id== request.Id);
            if (student == null)
            {
                throw new Exception("Ne postoji student sa ID:" + student.id);
            }
            student.Obrisan = true;

            await _context.SaveChangesAsync(cancellationToken);
            return new StudentSoftDeleteResponse
            {
                Id = student.id
            };
        }
    }
}
