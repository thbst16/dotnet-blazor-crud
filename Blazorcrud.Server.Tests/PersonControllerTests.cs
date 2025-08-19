using Xunit;
using Moq;
using Blazorcrud.Server.Controllers;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blazorcrud.Shared.Data;
using System.Collections.Generic;

namespace Blazorcrud.Server.Tests
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonRepository> _mockRepo;
        private readonly PersonController _controller;

        public PersonControllerTests()
        {
            _mockRepo = new Mock<IPersonRepository>();
            _controller = new PersonController(_mockRepo.Object);
        }

        [Fact]
        public void GetPeople_Returns_OkResult_With_PagedResult_Of_Person()
        {
            // Arrange
            var name = "Test";
            var page = 1;
            var pagedResult = new PagedResult<Person> { Results = new List<Person> { new Person { PersonId = 1, FirstName = "Test" } } };
            _mockRepo.Setup(repo => repo.GetPeople(name, page))
                .Returns(pagedResult);

            // Act
            var result = _controller.GetPeople(name, page);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PagedResult<Person>>(okResult.Value);
            Assert.Single(returnValue.Results);
        }

        [Fact]
        public async Task GetPerson_Returns_OkResult_With_Person()
        {
            // Arrange
            var id = 1;
            var person = new Person { PersonId = id, FirstName = "Test" };
            _mockRepo.Setup(repo => repo.GetPerson(id))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.GetPerson(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(id, returnValue.PersonId);
        }

        [Fact]
        public async Task AddPerson_Returns_OkResult_With_Person()
        {
            // Arrange
            var person = new Person { PersonId = 1, FirstName = "Test" };
            _mockRepo.Setup(repo => repo.AddPerson(person))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.AddPerson(person);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(person.PersonId, returnValue.PersonId);
        }

        [Fact]
        public async Task UpdatePerson_Returns_OkResult_With_Person()
        {
            // Arrange
            var person = new Person { PersonId = 1, FirstName = "Test" };
            _mockRepo.Setup(repo => repo.UpdatePerson(person))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.UpdatePerson(person);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(person.PersonId, returnValue.PersonId);
        }

        [Fact]
        public async Task DeletePerson_Returns_OkResult_With_Person()
        {
            // Arrange
            var id = 1;
            var person = new Person { PersonId = id, FirstName = "Test" };
            _mockRepo.Setup(repo => repo.DeletePerson(id))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.DeletePerson(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(id, returnValue.PersonId);
        }
    }
}
