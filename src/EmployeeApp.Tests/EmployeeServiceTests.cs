using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.Employees;
using EmployeeApp.Services.Contracts.Employees.Dto;
using EmployeeApp.Services.Employees;
using FluentAssertions;
using Moq;
using Xunit;
using MapsterMapper;

namespace EmployeeApp.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public async Task GetByIdAsync_WhenEmployeeExists_ReturnsEmployeeDto()
    {
        // Arrange
        var employeeId = 10;

        var uow = new Mock<IContractUnitOfWork>(MockBehavior.Strict);
        var repo = new Mock<IEmployeeRepository>(MockBehavior.Strict);
        var mapper = new Mock<IMapper>(MockBehavior.Strict);

        var employee = new Employee { Id = employeeId, FirstName = "Dorian", LastName = "Coelho" };
        var dto = new EmployeeDto { Id = employeeId, FirstName = "Dorian", LastName = "Coelho" };

        uow.SetupGet(x => x.Employees).Returns(repo.Object);
        repo.Setup(x => x.GetByIdAsync(employeeId)).ReturnsAsync(employee);
        mapper.Setup(x => x.Map<EmployeeDto>(employee)).Returns(dto);

        var sut = new EmployeeService(uow.Object, mapper.Object);

        // Act
        var result = await sut.GetByIdAsync(employeeId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(employeeId);

        repo.Verify(x => x.GetByIdAsync(employeeId), Times.Once);
        mapper.Verify(x => x.Map<EmployeeDto>(employee), Times.Once);
        uow.VerifyNoOtherCalls();
        repo.VerifyNoOtherCalls();
        mapper.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetByIdAsync_WhenEmployeeDoesNotExist_Throws()
    {
        // Arrange
        var employeeId = 10;

        var uow = new Mock<IContractUnitOfWork>(MockBehavior.Strict);
        var repo = new Mock<IEmployeeRepository>(MockBehavior.Strict);
        var mapper = new Mock<IMapper>(MockBehavior.Strict);

        uow.SetupGet(x => x.Employees).Returns(repo.Object);
        repo.Setup(x => x.GetByIdAsync(employeeId)).ReturnsAsync((Employee?) null);

        var sut = new EmployeeService(uow.Object, mapper.Object);

        // Act
        Func<Task> act = () => sut.GetByIdAsync(employeeId);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();

        repo.Verify(x => x.GetByIdAsync(employeeId), Times.Once);
        mapper.VerifyNoOtherCalls();
        uow.VerifyNoOtherCalls();
        repo.VerifyNoOtherCalls();
    }
}