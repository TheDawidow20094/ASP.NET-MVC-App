using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Queries.GetCarWorkshopServices
{
    public class GetCarworkshopServicesQuery : IRequest<IEnumerable<CarWorkshopServiceDto>>
    {
        public string EncodedName { get; set; } = default!;
    }
}
