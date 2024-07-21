using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Repositories;
using Airline.Services.CouponAPI.Services.Implements;
using AutoMapper;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testing.xUnit.Services
{
    public class BaggageServiceTest
    {
        private readonly Mock<IBaggageRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly BaggageService _service;

        public BaggageServiceTest()
        {
            _mockRepository = new Mock<IBaggageRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaggageCreateDTO, Baggage>();
                cfg.CreateMap<Baggage, BaggageDTO>();
            });
            _mapper = mapperConfig.CreateMapper();

            _service = new BaggageService(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_CreatesBaggage()
        {
            // Arrange
            var baggageCreateDTO = new BaggageCreateDTO { Name = "Bag1", Description = "Description1" };
            var baggage = new Baggage { BaggageId = 1, Name = "Bag1", Description = "Description1" };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Baggage>())).Returns(Task.CompletedTask);

            // Act
            await _service.CreateAsync(baggageCreateDTO);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Baggage>(b => b.Name == baggageCreateDTO.Name && b.Description == baggageCreateDTO.Description)), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfBaggageDTOs()
        {
            // Arrange
            var baggages = new List<Baggage>
            {
                new Baggage { BaggageId = 1, Name = "Bag1", Description = "Description1" },
                new Baggage { BaggageId = 2, Name = "Bag2", Description = "Description2" }
            };
            var baggageDTOs = _mapper.Map<IEnumerable<BaggageDTO>>(baggages);
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(baggages);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            var resultList = result.ToList();
            var expectedList = baggageDTOs.ToList();

            Assert.Equal(expectedList.Count, resultList.Count);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.Equal(expectedList[i].BaggageId, resultList[i].BaggageId);
                Assert.Equal(expectedList[i].Name, resultList[i].Name);
                Assert.Equal(expectedList[i].Description, resultList[i].Description);
                Assert.Equal(expectedList[i].Ticket_Baggages, resultList[i].Ticket_Baggages);
            }
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsBaggageDTO()
        {
            // Arrange
            var baggage = new Baggage { BaggageId = 1, Name = "Bag1", Description = "Description1" };
            var baggageDTO = _mapper.Map<BaggageDTO>(baggage);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(baggage);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(baggageDTO.BaggageId, result.BaggageId);
            Assert.Equal(baggageDTO.Name, result.Name);
            Assert.Equal(baggageDTO.Description, result.Description);
            Assert.Equal(baggageDTO.Ticket_Baggages, result.Ticket_Baggages);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesBaggage()
        {
            // Arrange
            var baggage = new Baggage { BaggageId = 1, Name = "OldName", Description = "OldDescription" };
            var baggageCreateDTO = new BaggageCreateDTO { Name = "UpdatedName", Description = "UpdatedDescription" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(baggage);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Baggage>())).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(1, baggageCreateDTO);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Baggage>(b => b.Name == "UpdatedName" && b.Description == "UpdatedDescription")), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesBaggage()
        {
            // Arrange
            var baggage = new Baggage { BaggageId = 1, Name = "Bag1", Description = "Description1" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(baggage);
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Baggage>())).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(baggage), Times.Once);
        }
    }
}
