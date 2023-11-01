using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandValidator : AbstractValidator<EditCarWorkshopCommand>
    {
        public EditCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(n => n.Description)
                .NotEmpty();

            RuleFor(n => n.PhoneNo)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
