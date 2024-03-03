using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.StudentEndpoints.StudentEdit
{
    [Route("Student")]
    public class StudentEditEndpoint : MyBaseEndpoint<StudentEditRequest, StudentEditResponse>
    {
        private readonly ApplicationDbContext _context;

        public StudentEditEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Edit")]
        public override async Task<StudentEditResponse> Obradi(StudentEditRequest request, CancellationToken cancellationToken)
        {
            Student student;
            if (request.Id == 0)
            {
                student= new Student();
                _context.Student.Add(student);
            }
            else
            {
                student= _context.Student.FirstOrDefault(x=>x.id==request.Id);
                if(student==null)
                {
                    throw new Exception("Ovaj student ne postoji");
                }
            }
            student.ime = request.Ime;
            student.prezime = request.Prezime;
            student.opstina_rodjenja_id = request.OpstinaId;
            await _context.SaveChangesAsync(cancellationToken:cancellationToken);

            return new StudentEditResponse { Id = student.id };

        }
    }
}
