using AutoMapper;
using LoanSimulator.Application.DTOs;
using LoanSimulator.Domain.Entities;
using LoanSimulator.Domain.Interfaces;
using LoanSimulator.Domain.Services;

namespace LoanSimulator.Application.Services;

public class LoanApplicationService
{
    private readonly LoanService _loanService;
    private readonly IMapper _mapper;
    private readonly ILoanRepository _loanRepository;

    public LoanApplicationService(LoanService loanService, IMapper mapper, ILoanRepository loanRepository)
    {
        _loanService = loanService;
        _mapper = mapper;
        _loanRepository = loanRepository;
    }

    public async Task<LoanResponseDto> SimulateLoanAsync(LoanRequestDto request)
    {
        if (request.LoanAmount <= 0)
            throw new ArgumentException("Loan amount must be greater than zero");

        if (request.AnnualInterestRate < 0)
            throw new ArgumentException("Annual interest rate must be non-negative");

        if (request.NumberOfMonths <= 0)
            throw new AggregateException("Number of months must be greater than zero");

        decimal monthlyPayment = CalculateMonthlyPayment(request.LoanAmount, request.AnnualInterestRate, request.NumberOfMonths);
        decimal totalInterest = CalculateTotalInterest(monthlyPayment, request.NumberOfMonths, request.LoanAmount);
        decimal totalPayment = CalculateTotalPayment(monthlyPayment, request.NumberOfMonths, request.LoanAmount);

        var paymentFlowSummary = new List<PaymentFlowSummaryDto>();
        decimal balance = request.LoanAmount;

        for (int month = 1; month <= request.NumberOfMonths; month++)
        {
            decimal interest = CalculateInterest(balance, request.AnnualInterestRate);
            decimal principal = Math.Round(monthlyPayment - interest, 2);

            balance = Math.Round(balance - principal, 2);

            paymentFlowSummary.Add(new PaymentFlowSummaryDto
            {
                PaymentMonth = month,
                Principal = principal,
                Interest = interest,
                Balance = balance,
                TotalInterest = totalInterest,
                TotalPayment = totalPayment
            });
        }

        var loan = new Loan
        {
            LoanAmount = request.LoanAmount,
            AnnualInterestRate = request.AnnualInterestRate,
            NumberOfMonths = request.NumberOfMonths,
            PaymentFlowSummary = paymentFlowSummary.Select(ps => new PaymentFlowSummary
            {
                PaymentMonth = ps.PaymentMonth,
                Principal = ps.Principal,
                Interest = ps.Interest,
                Balance = ps.Balance,
                TotalInterest = ps.TotalInterest,
                TotalPayment = ps.TotalPayment,
                LoanId = ps.LoanId
            }).ToList()
        };

        await _loanRepository.AddLoanAsync(loan);

        return new LoanResponseDto
        {
            MonthlyPayment = Math.Round(monthlyPayment, 2),
            TotalInterest = Math.Round(totalInterest, 2),
            TotalPayment = Math.Round(totalPayment, 2),
            PaymentFlowSummary = paymentFlowSummary
        };
    }

    private decimal CalculateMonthlyPayment(decimal loanAmount, decimal annualInterestRate, int numberOfMonths)
    {
        if (annualInterestRate <= 0)
            return loanAmount / numberOfMonths;

        decimal monthlyInterestRate = (annualInterestRate / 100) / 12;
        decimal factor = (decimal)Math.Pow((double)(1 + monthlyInterestRate), -numberOfMonths);
        decimal monthlyPayment = loanAmount * monthlyInterestRate / (1 - factor);

        return Math.Round(monthlyPayment, 2);
    }

    private decimal CalculateTotalInterest(decimal monthlyPayment, int numberOfMonths, decimal loanAmount)
    {
        return (monthlyPayment * numberOfMonths) - loanAmount;
    }

    private decimal CalculateTotalPayment(decimal monthlyPayment, int numberOfMonths, decimal loanAmount)
    {
        return Math.Round(monthlyPayment * numberOfMonths, 2);
    }

    private decimal CalculateInterest(decimal balance, decimal annualInterestRate)
    {
        decimal monthlyInterestRate = annualInterestRate / 100 / 12;
        return Math.Round(balance * monthlyInterestRate, 2);
    }
}
