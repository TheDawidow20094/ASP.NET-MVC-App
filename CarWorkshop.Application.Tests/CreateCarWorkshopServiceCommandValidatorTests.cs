using CarWorkshop.Application.CarWorkshopService.Commands;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarWorkshop.Application.Tests
{
    public class CreateCarWorkshopServiceCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidatonErrors()
        {
            //arrange
            var validator = new CreateCarWorkshopServiceCommandValidator();
            var command = new CreateCarWorkshopServiceCommand()
            {
                Price = 100,
                Description = "Description",
                CarWorkshopEncodedName = "workshop1"
            };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory()]        
        [InlineData(100, "", "1")]
        [InlineData(100, "test", null)]
        [InlineData(null, "test", "1")]
        [InlineData(null, "", null)]
        public void Validate_WithInvalidCommand_ShouldHaveValidatonErrors(decimal price, string description, string carWorkshopEncodedName)
        {
            //arrange
            var validator = new CreateCarWorkshopServiceCommandValidator();
            var command = new CreateCarWorkshopServiceCommand()
            {
                Price = 100,
                Description = "Description",
                CarWorkshopEncodedName = "workshop1"
            };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
