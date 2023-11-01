using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName
{
    public class GetCarWorkshopByEncodedNameQueryHandler : IRequestHandler<GetCarWorkshopByEncodedNameQuery, CarWorkshopDto>
    {
        readonly ICarWorkshopRepository _carWorkshopRepository;
        readonly IMapper _mapper;

        public GetCarWorkshopByEncodedNameQueryHandler(ICarWorkshopRepository carWorkshopRepository, IMapper mapper)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
        }

        public async Task<CarWorkshopDto> Handle(GetCarWorkshopByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetWorkshopByEncodedName(request.EncodedName);
            var dto = _mapper.Map<CarWorkshopDto>(carWorkshop);
            return dto;
        }
    }
}
