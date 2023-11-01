using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Please insert name")
                .MinimumLength(2)
                .MaximumLength(20)
                .Custom((value, context) =>
                {
                    var existingCarWorkshop = repository.GetWorkshopByName(value).Result;
                    if (existingCarWorkshop != null)
                    {
                        context.AddFailure($"Workshop with name: '{value}' already exist!");
                    }
                }
                );

            RuleFor(n => n.Description)
                .NotEmpty();

            RuleFor(n => n.PhoneNo)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
