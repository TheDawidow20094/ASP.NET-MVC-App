using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarWorkshop.Application.Tests
{
    public class CarWorkshopMapingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser()).Returns(new CurrentUser("1", "test@test", new List<string> { "User"}));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));
            var mapper = configuration.CreateMapper();
            var dto = new CarWorkshopDto
            {
                City = "Poznan",
                PhoneNo = "+48999999999",
                PostalCode = "55-555",
                Street = "Street"
            };

            //act
            var result = mapper.Map<Domain.Entities.CarWorkshop>(dto);

            //assert
            result.Should().NotBeNull();
            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PhoneNo.Should().Be(dto.PhoneNo);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);
        }

        [Theory()]
        [InlineData("1", true, "User")]
        [InlineData("99", false, "User")]
        [InlineData("99", true, "Moderator")]
        public void MappingProfile_ShouldMapCarWorkshopToCarWorkshopDto(string userId, bool shouldCanEdit, string role)
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser()).Returns(new CurrentUser(userId, "test@test", new List<string> { role }));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));
            var mapper = configuration.CreateMapper();
            var carWorkshop = new Domain.Entities.CarWorkshop
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new Domain.Entities.CarWorkshopContactDetails
                {
                    City = "Poznan",
                    PhoneNo = "+48999999999",
                    PostalCode = "55-555",
                    Street = "Street"
                }
            };

            //act
            var result = mapper.Map<CarWorkshopDto>(carWorkshop);

            //assert
            result.Should().NotBeNull();
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
            result.PhoneNo.Should().Be(carWorkshop.ContactDetails.PhoneNo);
            result.CanEdit.Should().Be(shouldCanEdit);
        }
    }
}
