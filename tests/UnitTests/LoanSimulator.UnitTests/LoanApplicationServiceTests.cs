using AutoMapper;
using LoanSimulator.Application.DTOs;
using LoanSimulator.Application.Services;
using LoanSimulator.Domain.Entities;
using LoanSimulator.Domain.Interfaces;
using Moq;

namespace LoanSimulator.UnitTests;

public class LoanApplicationServiceTests
{
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly LoanApplicationService _loanService;

    public LoanApplicationServiceTests()
    {
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _mapperMock = new Mock<IMapper>();

        _loanService = new LoanApplicationService(
            null,
            _mapperMock.Object,
            _loanRepositoryMock.Object
        );
    }

    [Fact]
    public async Task SimulateLoadAsync_ValidRequest_ReturnsCorrectResponse()
    {
        var request = new LoanRequestDto
        {
            LoanAmount = 50000,
            AnnualInterestRate = 12,
            NumberOfMonths = 24
        };

        _mapperMock.Setup(m => m.Map<LoanResponseDto>(It.IsAny<Loan>()))
            .Returns((Loan loan) => 
            {
                return new LoanResponseDto
                {
                    MonthlyPayment = Math.Round(loan.PaymentFlowSummary.First().Principal + loan.PaymentFlowSummary.First().Interest, 2),
                    TotalInterest = Math.Round(loan.PaymentFlowSummary.Sum(p => p.Interest), 2),
                    TotalPayment = Math.Round(loan.PaymentFlowSummary.Sum(p => p.Principal + p.Interest), 2),
                    PaymentFlowSummary = loan.PaymentFlowSummary.Select(ps => new PaymentFlowSummaryDto
                    {
                        PaymentMonth = ps.PaymentMonth,
                        Principal = ps.Principal,
                        Interest = ps.Interest,
                        Balance = ps.Balance
                    }).ToList()
                };
            });

        var response = await _loanService.SimulateLoanAsync(request);

        Assert.NotNull(response);
        Assert.Equal(2353.67m, response.MonthlyPayment);
        Assert.Equal(6488.08m, response.TotalInterest);
        Assert.Equal(56488.08m, response.TotalPayment);

        Assert.NotNull(response.PaymentFlowSummary);
        Assert.Equal(24, response.PaymentFlowSummary.Count);
        Assert.Equal(1, response.PaymentFlowSummary.First().PaymentMonth);
        Assert.Equal(0.07m, response.PaymentFlowSummary.Last().Balance);
    }

    [Fact]
    public async Task SimulateLoanAsync_InvalidLoanAmount_ThrowsArgumentException()
    {
        var request = new LoanRequestDto
        {
            LoanAmount = -1,
            AnnualInterestRate = 12,
            NumberOfMonths = 24
        };

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _loanService.SimulateLoanAsync(request)
        );

        Assert.Equal("Loan amount must be greater than zero", exception.Message);
    }
}
