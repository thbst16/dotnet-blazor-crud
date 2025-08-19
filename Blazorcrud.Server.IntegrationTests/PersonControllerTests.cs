using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazorcrud.Server;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Blazorcrud.Server.IntegrationTests
{
    public class PersonControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PersonControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_People_Returns_Success_And_Content()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/person?page=1");

            // Assert
            response.EnsureSuccessStatusCode();
            var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<Person>>();
            Assert.NotNull(pagedResult);
            Assert.IsType<PagedResult<Person>>(pagedResult);
        }
    }
}
