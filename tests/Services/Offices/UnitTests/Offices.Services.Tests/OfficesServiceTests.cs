using AutoFixture;
using AutoMapper;
using Moq;
using Offices.Contracts.DTOs;
using Offices.Domain.Entities;
using Offices.Domain.Interfaces;
using Offices.Services.Services;

namespace Offices.Services.Tests;

public class OfficesServiceTests
{
    private Fixture _fixture;
    private Mock<IOfficesRepository> _officeRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private OfficesService _officeService;

    public OfficesServiceTests()
    {
        _fixture = new Fixture();
        _officeRepositoryMock = new Mock<IOfficesRepository>();
        _mapperMock = new Mock<IMapper>();
        _officeService = new OfficesService(_officeRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async void GetAllOfficesAsync_OnSuccess_ReturnsListOfOfficeType()
    {
        //Arrange
        var FAKE_RESPONSE_FROM_REPOSITORY = _fixture.Create<List<Office>>();
        var FAKE_MAP_METHOD_RESULT = _fixture.Create<List<OfficeResponseDTO>>();
        _officeRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(FAKE_RESPONSE_FROM_REPOSITORY);
        _mapperMock.Setup(x => x.Map<List<OfficeResponseDTO>>(FAKE_RESPONSE_FROM_REPOSITORY))
            .Returns(FAKE_MAP_METHOD_RESULT);

        //Act
        var result = await _officeService.GetAllOfficesAsync();

        //Assert
        _officeRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        Assert.IsAssignableFrom<List<OfficeResponseDTO>>(result);
    }
}
