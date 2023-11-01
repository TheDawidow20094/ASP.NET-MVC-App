using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.Domain.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarWorkshop.Application.Tests
{
    public class CreateCarWorkshopServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreatesCarWorkshopService_WhenUserIsAuthorized()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            var command = new CreateCarWorkshopServiceCommand()
            {
                Price = 100,
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser()).Returns(new CurrentUser("1", "test@test", new List<string> { "User"}));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetWorkshopByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(Unit.Value);
            //Sprawdza czy metoda CreateService została w ogóle wywołana
            carWorkshopServiceRepositoryMock.Verify(m => m.CreateService(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);

        }

        [Fact()]
        public async Task Handle_CreatesCarWorkshopService_WhenUserIsModerator()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            var command = new CreateCarWorkshopServiceCommand()
            {
                Price = 100,
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser()).Returns(new CurrentUser("99", "test@test", new List<string> { "Moderator" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetWorkshopByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(Unit.Value);
            //Sprawdza czy metoda CreateService została w ogóle wywołana
            carWorkshopServiceRepositoryMock.Verify(m => m.CreateService(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);

        }

        [Fact()]
        public async Task Handle_DoNotCreatesCarWorkshopService_WhenUserIsNotAuthorized()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            var command = new CreateCarWorkshopServiceCommand()
            {
                Price = 100,
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser()).Returns(new CurrentUser("99", "test@test", new List<string> { "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetWorkshopByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(Unit.Value);
            
            carWorkshopServiceRepositoryMock.Verify(m => m.CreateService(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);

        }

        [Fact()]
        public async Task Handle_DoNotCreatesCarWorkshopService_WhenUserIsNotAutenticated()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            var command = new CreateCarWorkshopServiceCommand()
            {
                Price = 100,
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser()).Returns((CurrentUser?)null);

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetWorkshopByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(Unit.Value);

            carWorkshopServiceRepositoryMock.Verify(m => m.CreateService(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);

        }
    }
}
