using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Queries.GetCarWorkshopServices
{
    public class GetCarworkshopServicesQueryHandler : IRequestHandler<GetCarworkshopServicesQuery, IEnumerable<CarWorkshopServiceDto>>
    {
        readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;
        readonly IMapper _mapper;

        public GetCarworkshopServicesQueryHandler(ICarWorkshopServiceRepository carWorkshopServiceRepository, IMapper mapper)
        {
            _carWorkshopServiceRepository = carWorkshopServiceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetCarworkshopServicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _carWorkshopServiceRepository.GetAllCarWorkshopServicesByEncodedName(request.EncodedName);
            return _mapper.Map<IEnumerable<CarWorkshopServiceDto>>(result);
        }
    }
}
