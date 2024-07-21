using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Controllers;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Testing.xUnit.Controllers
{
    public class BaggageControllerTest
    {
        private readonly Mock<IBaggageService> _mockBaggageService;
        private readonly BaggageController _controller;

        public BaggageControllerTest()
        {
            _mockBaggageService = new Mock<IBaggageService>();
            _controller = new BaggageController(_mockBaggageService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfBaggageDTO()
        {
            // Arrange
            var baggages = new List<BaggageDTO>
            {
                new BaggageDTO { BaggageId = 1, Name = "Bag1", Description = "Description1" },
                new BaggageDTO { BaggageId = 2, Name = "Bag2", Description = "Description2" }
            };
            _mockBaggageService.Setup(service => service.GetAllAsync()).ReturnsAsync(baggages);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check the outer result type
            var returnBaggages = Assert.IsType<List<BaggageDTO>>(okResult.Value); // Check the inner result value
            Assert.Equal(baggages.Count, returnBaggages.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithBaggageDTO()
        {
            // Arrange
            var baggage = new BaggageDTO { BaggageId = 1, Name = "Bag1", Description = "Description1" };
            _mockBaggageService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(baggage);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<BaggageDTO>>(result); 
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var returnBaggage = Assert.IsType<BaggageDTO>(returnValue.Value);
            Assert.Equal(baggage.BaggageId, returnBaggage.BaggageId);
            Assert.Equal(baggage.Name, returnBaggage.Name);
            Assert.Equal(baggage.Description, returnBaggage.Description);
        }


        [Fact]
        public async Task Create_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var baggageCreateDTO = new BaggageCreateDTO { Name = "NewBag", Description = "NewDescription" };
            _mockBaggageService.Setup(service => service.CreateAsync(baggageCreateDTO))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(baggageCreateDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Baggage created successfully", okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenBaggageExists()
        {
            // Arrange
            _mockBaggageService.Setup(service => service.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Baggage deleted successfully", okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenBaggageExists()
        {
            // Arrange
            var baggageCreateDTO = new BaggageCreateDTO { Name = "UpdatedBag", Description = "UpdatedDescription" };
            _mockBaggageService.Setup(service => service.UpdateAsync(1, baggageCreateDTO))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(1, baggageCreateDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Baggage updated successfully", okResult.Value);
        }
    }
}
