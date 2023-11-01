using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshopService;
using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Application.Mappings
{
    public class CarWorkshopMappingProfile : Profile
    {
        public CarWorkshopMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            //Mapujemy prop Obiektu, nie trzeba mapować prop, których nazwy oraz typy się pokrywają
            CreateMap<CarWorkshopDto, Domain.Entities.CarWorkshop>().ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails()
            {
                City = src.City,
                PhoneNo = src.PhoneNo,
                PostalCode = src.PostalCode,
                Street = src.Street,
            }));

            //Mapowanie odwrotne, zmienne z CarWorkhsopDto do obiektu CarWorkshopContactDetails w CarWorkshop
            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDto>()
                .ForMember(dto => dto.CanEdit, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id || user.IsInRole("Moderator"))))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.PhoneNo, opt => opt.MapFrom(src => src.ContactDetails.PhoneNo));

            //Wszystkie właściwości pokrywają się 1:1
            CreateMap<CarWorkshopDto, EditCarWorkshopCommand>();

            //Mapowanie CarWorkshop z CarworkshopService
            CreateMap<CarWorkshopServiceDto, Domain.Entities.CarWorkshopService>()
                .ReverseMap();// automatyczne odwrócenie mapowania
        }
    }
}
