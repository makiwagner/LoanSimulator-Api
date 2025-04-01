namespace LoanSimulator.Domain.Entities;

public class Loan
{
    public int Id { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal AnnualInterestRate { get; set; }
    public int NumberOfMonths { get; set; }
    public ICollection<PaymentFlowSummary> PaymentFlowSummary { get; set; } = new List<PaymentFlowSummary>();
}