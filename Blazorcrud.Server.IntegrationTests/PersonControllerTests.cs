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

        [Fact]
        public async Task Get_Person_By_Id_Returns_Success_And_Content()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/person/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<Person>();
            Assert.NotNull(person);
            Assert.Equal(1, person.PersonId);
        }

        [Fact]
        public async Task Add_Person_Returns_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await Utilities.GetJwtAsync(client);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var newPerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = Gender.Male,
                PhoneNumber = "1234567890",
                Addresses = new()
                {
                    new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" }
                }
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/person", newPerson);

            // Assert
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<Person>();
            Assert.NotNull(person);
            Assert.Equal("John", person.FirstName);
        }

        [Fact]
        public async Task Update_Person_Returns_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await Utilities.GetJwtAsync(client);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var newPerson = new Person
            {
                FirstName = "Jane",
                LastName = "Doe",
                Gender = Gender.Female,
                PhoneNumber = "1234567890",
                Addresses = new()
                {
                    new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" }
                }
            };
            var response = await client.PostAsJsonAsync("/api/person", newPerson);
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<Person>();

            // Act
            person.FirstName = "Janet";
            response = await client.PutAsJsonAsync("/api/person", person);

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedPerson = await response.Content.ReadFromJsonAsync<Person>();
            Assert.NotNull(updatedPerson);
            Assert.Equal("Janet", updatedPerson.FirstName);
        }

        [Fact]
        public async Task Delete_Person_Returns_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await Utilities.GetJwtAsync(client);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var newPerson = new Person
            {
                FirstName = "Jim",
                LastName = "Doe",
                Gender = Gender.Male,
                PhoneNumber = "1234567890",
                Addresses = new()
                {
                    new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" }
                }
            };
            var response = await client.PostAsJsonAsync("/api/person", newPerson);
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<Person>();

            // Act
            response = await client.DeleteAsync($"/api/person/{person.PersonId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
