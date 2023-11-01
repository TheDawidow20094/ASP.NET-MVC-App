using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
    {
        readonly IUserContext _userContext;
        readonly ICarWorkshopRepository _carWorkshopRepository;
        readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

        public CreateCarWorkshopServiceCommandHandler(IUserContext userContext, ICarWorkshopRepository carWorkshopRepository, ICarWorkshopServiceRepository carWorkshopServiceRepository)
        {
            _userContext = userContext;
            _carWorkshopRepository = carWorkshopRepository;
            _carWorkshopServiceRepository = carWorkshopServiceRepository;
        }

        public async Task<Unit> Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetWorkshopByEncodedName(request.CarWorkshopEncodedName);
            var user = _userContext.GetCurrentUser();
            var canEdit = user != null && (user.Id == carWorkshop.CreatedById || user.IsInRole("Moderator"));
            if (canEdit) 
            {
                var carWorkshopService = new Domain.Entities.CarWorkshopService()
                {
                    Description = request.Description,
                    Price = request.Price,
                    CarWorkshopId = carWorkshop.Id,
                };
                await _carWorkshopServiceRepository.CreateService(carWorkshopService);
            }     
            return Unit.Value;
        }
    }
}
