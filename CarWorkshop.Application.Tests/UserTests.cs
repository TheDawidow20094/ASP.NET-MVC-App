using CarWorkshop.Application.ApplicationUser;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace CarWorkshop.Application.Tests
{
    public class UserTests
    {
        [Theory]
        [InlineData("Admin", true)]
        [InlineData("User", true)]
        [InlineData("Moderator", false)]
        [InlineData("admin", false)]
        public void CheckUserInRole(string role, bool shouldBeInRole)
        {
            //arrange
            var user = new CurrentUser("1", "test@test", new List<string> { "Admin", "User"});

            //act
            bool result = user.IsInRole(role);

            //assert
            result.Should().Be(shouldBeInRole);
        }

        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test");
            currentUser.Roles.Should().ContainInOrder("Admin", "User");
        }
    }    
}
