using Xunit;
using CarWorkshop.Domain.Entities;
using FluentAssertions;

namespace CarWorkshop.Domain.Tests
{
    public class CarWorkshopTests
    {
        [Fact()]
        public void EndodeNameTest_ShouldSetEncodedName()
        {
            //arrange
            var carWorkshop = new CarWorkshop.Domain.Entities.CarWorkshop();
            carWorkshop.Name = "Test Workshop";

            //act
            carWorkshop.EncodeName();

            //assert            
            carWorkshop.EncodedName.Should().Be("test-workshop");

        }

        [Fact()]
        public void EncodedName_ShouldThrowException_WhenNameIsNull()
        {
            //arrange
            var carWorkshop = new CarWorkshop.Domain.Entities.CarWorkshop();

            //act
            Action action = () => carWorkshop.EncodeName();

            //assert
            action.Invoking(a => a.Invoke()).Should().Throw<NullReferenceException>();

        }
    }
}
