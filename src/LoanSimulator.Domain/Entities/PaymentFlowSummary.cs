namespace LoanSimulator.Domain.Entities;

public class PaymentFlowSummary
{
    public int Id { get; set; }
    public int PaymentMonth { get; set; }
    public decimal Principal { get; set; }
    public decimal Interest { get; set; }
    public decimal Balance { get; set; }
    public decimal TotalInterest { get; set; }
    public decimal TotalPayment { get; set; }
    public int LoanId { get; set; }
    public Loan? Loan { get; set; }
}