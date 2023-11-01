using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        readonly ICarWorkshopRepository _carWorkshopRepository;
        readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, IUserContext userContext)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetWorkshopByEncodedName(request.Name);
            var user = _userContext.GetCurrentUser();
            bool canEdit = user != null && carWorkshop.CreatedById == user.Id || user.IsInRole("Moderator");
            if (canEdit == false)
            {
                throw new Exception("You have no access!");
            }
            carWorkshop.Description = request.Description;
            carWorkshop.About = request.About;
            carWorkshop.ContactDetails.City = request.City;
            carWorkshop.ContactDetails.PhoneNo = request.PhoneNo;
            carWorkshop.ContactDetails.PostalCode = request.PostalCode;
            carWorkshop.ContactDetails.Street = request.Street;
            await _carWorkshopRepository.Commit();
            return Unit.Value;
        }
    }
}
