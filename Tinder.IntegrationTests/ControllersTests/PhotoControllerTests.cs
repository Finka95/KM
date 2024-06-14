using Shouldly;
using System.Net.Http.Json;
using System.Net;
using Tinder.API.DTO.CreateDto;
using Tinder.API.Models;
using Tinder.IntegrationTests.Attributes;

namespace Tinder.IntegrationTests.ControllersTests
{
    public class PhotoControllerTests : BaseTestClass
    {
        public readonly Guid UserId = new Guid("852c8e71-f6d9-44f1-a649-8de568a2023c");
        public readonly Guid PhotoId = new Guid("e222a1fb-7a59-43d4-af7a-ceb18acdfebe");
        public PhotoControllerTests(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetAll_HasData_ReturnsList()
        {
            // Act
            var response = await _httpClient.GetAsync($"/{UserId}/photos");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<List<PhotoDto>>();
            response.EnsureSuccessStatusCode();
            result.ShouldNotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public async Task GetById_ValidId_ShouldReturnPhotoDto(
            CreateUserDto createUserDto,
            CreatePhotoDto createPhotoDto
            )
        {
            // Arrange
            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            var createdPhotoResponse = await _httpClient.PostAsJsonAsync($"/{createdUser.Id}/photos", createPhotoDto);
            var createdPhoto = await createdPhotoResponse.Content.ReadFromJsonAsync<PhotoDto>();

            // Act
            var response = await _httpClient.GetAsync($"/{createdUser.Id}/photos/{createdPhoto.Id}");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<PhotoDto>();
            response.EnsureSuccessStatusCode();
            result.Id.ShouldBeEquivalentTo(createdPhoto.Id);
            result.UserId.ShouldBeEquivalentTo(createdPhoto.UserId);
            result.UserId.ShouldBeEquivalentTo(createdUser.Id);
        }

        [Fact]
        public async Task GetById_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.GetAsync($"/{id}/photos/{PhotoId}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [AutoMoqData]
        [Theory]
        public async Task Create_ValidPhoto_ReturnPhotoDto(
            CreateUserDto createUserDto,
            CreatePhotoDto createPhotoDto
        )
        {
            // Arrange
            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            // Act
            var createdPhotoResponse = await _httpClient.PostAsJsonAsync($"/{createdUser.Id}/photos", createPhotoDto);

            // Assert
            var createdPhoto = await createdPhotoResponse.Content.ReadFromJsonAsync<PhotoDto>();
            createdPhotoResponse.EnsureSuccessStatusCode();
            createdPhoto.PhotoURL.ShouldBeEquivalentTo(createPhotoDto.PhotoURL);
            createdPhoto.UserId.ShouldBeEquivalentTo(createdUser.Id);
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_InvalidPhoto_ReturnBadRequest(
           CreateUserDto createUserDto
        )
        {
            // Arrange
            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            var invalidUserDto = new CreatePhotoDto
            {
                PhotoURL = null
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync($"{createdUser.Id}/photos", invalidUserDto);

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoMoqData]
        public async Task DeleteById_ValidId_ShouldReturnOk(
            CreateUserDto createUserDto,
            CreatePhotoDto createPhotoDto
        )
        {
            // Arrange
            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            var createdPhotoResponse = await _httpClient.PostAsJsonAsync($"/{createdUser.Id}/photos", createPhotoDto);
            var createdPhoto = await createdPhotoResponse.Content.ReadFromJsonAsync<PhotoDto>();
            // Act
            var response = await _httpClient.DeleteAsync($"{createdUser.Id}/photos/{createdPhoto.Id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [AutoMoqData]
        public async Task DeleteById_InvalidId_ShouldReturnOk(
            CreateUserDto createUserDto
        )
        {
            // Arrange
            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.DeleteAsync($"{createdUser.Id}/photos/{id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task UpdateAvatar_ValidId_ShouldReturnPhotoDto(
            CreateUserDto createUserDto,
            CreatePhotoDto createPhotoDto
        )
        {
            // Arrange

            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            createPhotoDto.IsAvatar = false;
            var createdPhotoResponse = await _httpClient.PostAsJsonAsync($"/{createdUser.Id}/photos", createPhotoDto);
            var createdPhoto = await createdPhotoResponse.Content.ReadFromJsonAsync<PhotoDto>();

            // Act
            var response = await _httpClient.PutAsync($"{createdUser.Id}/photos/{createdPhoto.Id}", default);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<PhotoDto>();
            response.EnsureSuccessStatusCode();

            result.Id.ShouldBeEquivalentTo(createdPhoto.Id);
            result.IsAvatar.ShouldBeTrue();
            result.PhotoURL.ShouldBeEquivalentTo(createdPhoto.PhotoURL);
        }

        [Theory]
        [AutoMoqData]
        public async Task UpdateAvatar_InvalidId_ShouldReturnNotFound(
            CreateUserDto createUserDto,
            CreatePhotoDto createPhotoDto
        )
        {
            // Arrange
            var createdUserResponse = await _httpClient.PostAsJsonAsync("api/users", createUserDto);
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserDto>();

            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.PutAsync($"{createdUser.Id}/photos/{id}", default);

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }
    }
}
