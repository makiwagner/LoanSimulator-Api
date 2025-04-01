namespace LoanSimulator.Application.DTOs;

public class LoanRequestDto
{
    public decimal LoanAmount { get; set; }
    public decimal AnnualInterestRate { get; set; }
    public int NumberOfMonths { get; set; }
}