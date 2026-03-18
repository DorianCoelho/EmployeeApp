using EmployeeApp.Domain.Core.Entities.ContractHistories;
using EmployeeApp.Domain.Core.Entities.Contracts;
using EmployeeApp.Domain.Core.Entities.Employees;
using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Infrastructure.Contracts.ContractHistories;
using EmployeeApp.Infrastructure.Contracts.Contracts;
using EmployeeApp.Services.Contracts;
using EmployeeApp.Services.Contracts.Contracts.Dto;
using EmployeeApp.Services.Contracts.Contracts.Request;
using FluentAssertions;
using Moq;
using Xunit;
using MapsterMapper;

namespace EmployeeApp.Tests;

public class ContractServiceTests
{
    [Fact]
    public async Task CreateContractAsync_CreatesContractAndHistory_AndCommits()
    {
        // Arrange
        var uow = new Mock<IContractUnitOfWork>(MockBehavior.Strict);
        var contractsRepo = new Mock<IContractRepository>(MockBehavior.Strict);
        var historyRepo = new Mock<IContractHistoryRepository>(MockBehavior.Strict);
        var mapper = new Mock<IMapper>(MockBehavior.Strict);

        uow.Setup(x => x.BeginAsync()).Returns(Task.CompletedTask);
        uow.SetupGet(x => x.Contracts).Returns(contractsRepo.Object);
        uow.SetupGet(x => x.ContractHistory).Returns(historyRepo.Object);
        uow.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);


        var request = new CreateContractRequest
        {
            EmployeeId = 5,
            StartDate = DateTime.Today,
            JobTitle = "Dev",
            Salary = 2000,
            WeeklyHours = 40,
            Type = ContractType.Indefinite,
            Status = ContractStatus.Active,
            WorkDayType = WorkDayType.FullTime
        };

        var filter = new CreateContractFilter
        {
            EmployeeId = request.EmployeeId,
            StartDate = request.StartDate,
            EndDate = null,
            JobTitle = request.JobTitle,
            Salary = request.Salary,
            WeeklyHours = request.WeeklyHours,
            Type = request.Type,
            Status = request.Status,
            WorkDayType = request.WorkDayType
        };

        mapper.Setup(m => m.Map<CreateContractFilter>(request)).Returns(filter);

        var createdContract = new Contract
        {
            Id = 99,
            Salary = request.Salary,
            WeeklyHours = request.WeeklyHours,
            JobTitle = request.JobTitle,
            Type = request.Type,
            Status = request.Status,
            WorkDayType = request.WorkDayType,
            Employee = new Employee { Id = request.EmployeeId }
        };

        contractsRepo.Setup(r => r.CreateContractAsync(filter)).ReturnsAsync(createdContract);

        historyRepo.Setup(r => r.CreateContractHistoryAsync(
                It.Is<CreateContractHistoryFilter>(h =>
                    h.ContractId == createdContract.Id &&
                    h.Salary == createdContract.Salary &&
                    h.WeeklyHours == createdContract.WeeklyHours &&
                    h.JobTitle == createdContract.JobTitle &&
                    h.Type == createdContract.Type &&
                    h.Status == createdContract.Status &&
                    h.WorkDayType == createdContract.WorkDayType &&
                    !string.IsNullOrWhiteSpace(h.Reason)
                )))
            .ReturnsAsync(new ContractHistory { Id = 1 });

        mapper.Setup(m => m.Map<ContractDto>(createdContract))
            .Returns(new ContractDto { Id = createdContract.Id, EmployeeId = request.EmployeeId });

        var sut = new ContractService(uow.Object, mapper.Object);

        // Act
        var dto = await sut.CreateContractAsync(request);

        // Assert
        dto.Id.Should().Be(99);

        uow.Verify(x => x.BeginAsync(), Times.Once);
        contractsRepo.Verify(r => r.CreateContractAsync(filter), Times.Once);
        historyRepo.Verify(r => r.CreateContractHistoryAsync(It.IsAny<CreateContractHistoryFilter>()), Times.Once);
        uow.Verify(x => x.CommitAsync(), Times.Once);

        mapper.Verify(m => m.Map<CreateContractFilter>(request), Times.Once);
        mapper.Verify(m => m.Map<ContractDto>(createdContract), Times.Once);

        uow.VerifyNoOtherCalls();
        contractsRepo.VerifyNoOtherCalls();
        historyRepo.VerifyNoOtherCalls();
        mapper.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task UpdateCompensationAsync_UpdatesSalaryAndHours_CreatesHistory_AndCommits()
    {
        // Arrange
        var uow = new Mock<IContractUnitOfWork>(MockBehavior.Strict);
        var contractsRepo = new Mock<IContractRepository>(MockBehavior.Strict);
        var historyRepo = new Mock<IContractHistoryRepository>(MockBehavior.Strict);
        var mapper = new Mock<IMapper>(MockBehavior.Strict);

        uow.Setup(x => x.BeginAsync()).Returns(Task.CompletedTask);
        uow.SetupGet(x => x.Contracts).Returns(contractsRepo.Object);
        uow.SetupGet(x => x.ContractHistory).Returns(historyRepo.Object);
        uow.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);
        uow.Setup(x => x.RollbackAsync()).Returns(Task.CompletedTask);

        var contract = new Contract
        {
            Id = 10,
            Salary = 1500,
            WeeklyHours = 40,
            JobTitle = "Dev",
            Type = ContractType.Indefinite,
            Status = ContractStatus.Active,
            WorkDayType = WorkDayType.FullTime,
            Employee = new Employee { Id = 5 }
        };

        contractsRepo.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(contract);

        contractsRepo
            .Setup(r => r.UpdateContractAsync(It.IsAny<Contract>()))
            .ReturnsAsync((Contract c) => contract);

        historyRepo.Setup(r => r.CreateContractHistoryAsync(It.Is<CreateContractHistoryFilter>(h =>
                h.ContractId == 10 &&
                h.Salary == 2000 &&
                h.WeeklyHours == 35 &&
                h.JobTitle == "Dev" &&
                h.Type == contract.Type &&
                h.Status == contract.Status &&
                h.WorkDayType == contract.WorkDayType &&
                h.Reason == "Ajuste"
            )))
            .ReturnsAsync(new ContractHistory { Id = 2 });

        var sut = new ContractService(uow.Object, mapper.Object);

        var request = new EditContractRequest
        {
            Id = 10,
            EmployeeId = 5,
            StartDate = DateTime.Today,
            EndDate = null,
            JobTitle = "Dev",
            Salary = 2000,
            WeeklyHours = 35,
            Type = ContractType.Indefinite,
            Status = ContractStatus.Active,
            WorkDayType = WorkDayType.FullTime,
            Reason = "Ajuste"
        };

        // Act
        await sut.UpdateContractAsync(request);

        // Assert (estado en memoria)
        contract.Salary.Should().Be(2000);
        contract.WeeklyHours.Should().Be(35);

        uow.Verify(x => x.BeginAsync(), Times.Once);
        contractsRepo.Verify(r => r.GetByIdAsync(10), Times.Once);

        // Verifica que Update se llama con un contrato que ya tiene los valores nuevos
        contractsRepo.Verify(r => r.UpdateContractAsync(It.Is<Contract>(c =>
            c.Id == 10 &&
            c.Salary == 2000 &&
            c.WeeklyHours == 35
        )), Times.Once);

        historyRepo.Verify(r => r.CreateContractHistoryAsync(It.Is<CreateContractHistoryFilter>(h =>
            h.ContractId == 10 &&
            h.Salary == 2000 &&
            h.WeeklyHours == 35 &&
            h.JobTitle == "Dev" &&
            h.Type == contract.Type &&
            h.Status == contract.Status &&
            h.WorkDayType == contract.WorkDayType &&
            h.Reason == "Ajuste"
        )), Times.Once);

        uow.Verify(r => r.CommitAsync(), Times.Once);
        uow.Verify(r => r.RollbackAsync(), Times.Never);

        uow.VerifyNoOtherCalls();
        contractsRepo.VerifyNoOtherCalls();
        historyRepo.VerifyNoOtherCalls();
        mapper.VerifyNoOtherCalls();
    }
}