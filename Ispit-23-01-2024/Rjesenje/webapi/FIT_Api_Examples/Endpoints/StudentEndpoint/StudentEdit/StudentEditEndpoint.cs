using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FIT_Api_Examples.Endpoints.StudentEndpoint.StudentEdit
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
            if(request.id== 0)
            {
                student = new Student();
                _context.Student.Add(student);
            }
            else { 
                
                student=_context.Student.FirstOrDefault(student=> student.id== request.id);
                if(student==null) {
                    throw new Exception("Ne postoji korisnik sa ID: " + student.id);
                }
                
            }
            student.ime = request.Ime;
            student.prezime = request.Prezime;
            student.opstina_rodjenja_id = request.OpstinaID;
            await _context.SaveChangesAsync(cancellationToken);

            return new StudentEditResponse
            {
                Id = student.id
            };
        }
    }
}
