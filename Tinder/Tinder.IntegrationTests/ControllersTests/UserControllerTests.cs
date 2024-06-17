using System.Net.Http.Json;
using System.Net;
using Shouldly;
using Tinder.API.DTO.CreateDto;
using Tinder.API.DTO.UpdateDto;
using Tinder.API.Models;
using Tinder.BLL.Models;
using Tinder.IntegrationTests.Attributes;

namespace Tinder.IntegrationTests.ControllersTests
{
    public class UserControllerTests : BaseTestClass
    {
        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetAll_HasData_ReturnsList()
        {
            // Act
            var response = await _httpClient.GetAsync("api/users");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<List<UserDto>>();
            response.EnsureSuccessStatusCode();
            result.ShouldNotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public async Task GetById_ValidId_ShouldReturnUserDto(
            CreateUserDto createUserDto
            )
        {
            // Arrange
            var createdResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdResponse.Content.ReadFromJsonAsync<UserDto>();

            // Act
            var response = await _httpClient.GetAsync($"api/users/{createdUser.Id}");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<User>();
            response.EnsureSuccessStatusCode();
            result.Id.ShouldBeEquivalentTo(createdUser.Id);
            result.FirstName.ShouldBeEquivalentTo(createdUser.FirstName);
            result.LastName.ShouldBeEquivalentTo(createUserDto.LastName);
        }

        [Fact]
        public async Task GetById_InvalidId_ShouldReturnNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.GetAsync($"api/users/{id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [AutoMoqData]
        [Theory]
        public async Task Create_ValidUser_ReturnUserDto(
            CreateUserDto createUserDto
            )
        {
            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/users", createUserDto);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<UserDto>();
            response.EnsureSuccessStatusCode();
            result.FirstName.ShouldBeEquivalentTo(createUserDto.FirstName);
            result.LastName.ShouldBeEquivalentTo(createUserDto.LastName);
        }

        [Theory]
        [InlineData(null, "valid", "valid", "valid")]
        [InlineData("valid", null, "valid", "valid")]
        [InlineData("valid", "valid", null, "valid")]
        [InlineData("valid", "valid", "valid", null)]
        public async Task Create_InvalidUser_ReturnBadRequest(
            string firstName,
            string lastName,
            string description,
            string city
            )
        {
            // Arrange
            var invalidUserDto = new CreateUserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Description = description,
                City = city
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("api/users", invalidUserDto);

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoMoqData]
        public async Task DeleteById_ValidId_ShouldReturnOk(
            CreateUserDto createUserDto
            )
        {
            // Arrange
            var createResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createResponse.Content.ReadFromJsonAsync<UserDto>();

            // Act
            var response = await _httpClient.DeleteAsync($"api/users/{createdUser.Id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteById_InvalidId_ShouldReturnNotFound()
        {
            // Arrange 
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.DeleteAsync($"api/users/{id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task UpdateById_ValidUpdateUserDto_ShouldReturnUserDto(
            CreateUserDto createUserDto,
            UpdateUserDto updateUserDto
            )
        {
            // Arrange
            var createResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createResponse.Content.ReadFromJsonAsync<UserDto>();

            // Act
            var response = await _httpClient.PutAsJsonAsync($"api/users/{createdUser.Id}", updateUserDto);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<UserDto>();
            response.EnsureSuccessStatusCode();

            result.Id.ShouldBeEquivalentTo(createdUser.Id);
            result.FirstName.ShouldBeEquivalentTo(updateUserDto.FirstName);
            result.LastName.ShouldBeEquivalentTo(updateUserDto.LastName);
            result.Age.ShouldBeEquivalentTo(updateUserDto.Age);
            result.City.ShouldBeEquivalentTo(updateUserDto.City);
            result.Description.ShouldBeEquivalentTo(updateUserDto.Description);
        }

        [Theory]
        [InlineData(null, "valid", "valid", "valid")]
        [InlineData("valid", null, "valid", "valid")]
        [InlineData("valid", "valid", null, "valid")]
        [InlineData("valid", "valid", "valid", null)]
        public async Task UpdateById_InvalidUser_ReturnBadRequest(
            string firstName,
            string lastName,
            string description,
            string city
        )
        {
            // Arrange
            var invalidUpdateUserDto = new UpdateUserDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Description = description,
                City = city
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("api/users", invalidUpdateUserDto);

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }

    }
}
