using Shouldly;
using System.Net.Http.Json;
using System.Net;
using Tinder.API.DTO.CreateDto;
using Tinder.API.Models;
using Tinder.IntegrationTests.Attributes;

namespace Tinder.IntegrationTests.ControllersTests
{
    public class LikeControllerTests : BaseTestClass
    {

        public LikeControllerTests(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Theory]
        [AutoMoqData]
        public async Task GetById_ValidId_ShouldReturnLikeDto(
            CreateUserDto createSenderDto,
            CreateUserDto createReceiverDto
            )
        {
            // Arrange
            var createdSenderResponse = await _httpClient.PostAsJsonAsync("api/users", createSenderDto);
            var createdSender = await createdSenderResponse.Content.ReadFromJsonAsync<UserDto>();

            var createdReceiverResponse = await _httpClient.PostAsJsonAsync("api/users", createReceiverDto);
            var createdReceiver = await createdReceiverResponse.Content.ReadFromJsonAsync<UserDto>();

            var createLikeDto = new CreateLikeDto
            {
                SenderId = createdSender.Id,
                ReceiverId = createdReceiver.Id
            };

            var createdLikeResponse = await _httpClient.PostAsJsonAsync("api/likes", createLikeDto);
            var createdLike = await createdLikeResponse.Content.ReadFromJsonAsync<LikeDto>();

            // Act
            var response = await _httpClient.GetAsync($"/api/likes/{createdLike.Id}");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<LikeDto>();
            result.Id.ShouldBeEquivalentTo(createdLike.Id);
            result.ReceiverId.ShouldBeEquivalentTo(createdLike.ReceiverId);
            result.SenderId.ShouldBeEquivalentTo(createdLike.SenderId);
        }

        [Fact]
        public async Task GetById_InvalidId_ShouldReturnNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.GetAsync($"/api/likes/{id}");

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }

        [AutoMoqData]
        [Theory]
        public async Task Create_ValidLike_ShouldReturnLikeDto(
            CreateUserDto createSenderDto,
            CreateUserDto createReceiverDto
            )
        {
            // Arrange
            var createdSenderResponse = await _httpClient.PostAsJsonAsync("api/users", createSenderDto);
            var createdSender = await createdSenderResponse.Content.ReadFromJsonAsync<UserDto>();

            var createdReceiverResponse = await _httpClient.PostAsJsonAsync("api/users", createReceiverDto);
            var createdReceiver = await createdReceiverResponse.Content.ReadFromJsonAsync<UserDto>();

            var createLikeDto = new CreateLikeDto
            {
                SenderId = createdSender.Id,
                ReceiverId = createdReceiver.Id
            };


            // Act
            var createdLikeResponse = await _httpClient.PostAsJsonAsync("api/likes", createLikeDto);

            // Assert
            createdLikeResponse.EnsureSuccessStatusCode();
            var createdLike = await createdLikeResponse.Content.ReadFromJsonAsync<LikeDto>();
            createdLike.ReceiverId.ShouldBeEquivalentTo(createdReceiver.Id);
            createdLike.SenderId.ShouldBeEquivalentTo(createdSender.Id);
        }

        [AutoMoqData]
        [Theory]
        public async Task Create_InvalidLike_ShouldReturnNotFound(
            CreateLikeDto createLikeDto
        )
        {
            // Act
            var createdLikeResponse = await _httpClient.PostAsJsonAsync("api/likes", createLikeDto);

            // Assert
            createdLikeResponse.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteById_ValidLike_ShouldDeleteLike(
            CreateUserDto createSenderDto,
            CreateUserDto createReceiverDto
        )
        {
            // Arrange
            var createdSenderResponse = await _httpClient.PostAsJsonAsync("api/users", createSenderDto);
            var createdSender = await createdSenderResponse.Content.ReadFromJsonAsync<UserDto>();

            var createdReceiverResponse = await _httpClient.PostAsJsonAsync("api/users", createReceiverDto);
            var createdReceiver = await createdReceiverResponse.Content.ReadFromJsonAsync<UserDto>();

            var createLikeDto = new CreateLikeDto
            {
                SenderId = createdSender.Id,
                ReceiverId = createdReceiver.Id
            };

            var createdLikeResponse = await _httpClient.PostAsJsonAsync("api/likes", createLikeDto);
            var createdLike = await createdLikeResponse.Content.ReadFromJsonAsync<LikeDto>();

            // Act
            var response = await _httpClient.DeleteAsync($"/api/likes/{createdLike.Id}");

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteById_InvalidLike_ShouldReturnNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.DeleteAsync($"/api/likes/{id}");

            // Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }
    }
}
