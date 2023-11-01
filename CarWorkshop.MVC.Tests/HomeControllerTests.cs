using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace CarWorkshop.MVC.Tests
{    
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly WebApplicationFactory<Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task About_ReturnsViewWithRenderModel()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync("/Home/About");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("<li>car</li>")
                .And.Contain("<li>app</li>")
                .And.Contain("<li>free</li>");
        }
    }
}
