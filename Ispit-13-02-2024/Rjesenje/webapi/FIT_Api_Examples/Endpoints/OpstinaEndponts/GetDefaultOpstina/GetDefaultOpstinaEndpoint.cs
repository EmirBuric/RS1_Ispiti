using FIT_Api_Example.Helper;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Endpoints.OpstinaEndponts.GetDefaultOpstina
{
    [Route("Opstina")]
    public class GetDefaultOpstinaEndpoint : MyBaseEndpoint<NoRequest, GetDefaultOpstinaResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetDefaultOpstinaEndpoint(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetDefault")]
        public override async Task<GetDefaultOpstinaResponse> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken)
        {
            var opstina= _context.Student.
                GroupBy(x=>x.opstina_rodjenja_id).
                OrderByDescending(x=>x.Count()).
                Select(x=>x.Key).
                FirstOrDefault();
            if(opstina == null) {
                throw new Exception("Nema Opstine");
            }
            return new GetDefaultOpstinaResponse
            {
                Id = opstina
            };
        }
    }
}
