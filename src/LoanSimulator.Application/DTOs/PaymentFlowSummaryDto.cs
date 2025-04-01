namespace LoanSimulator.Application.DTOs;

public class PaymentFlowSummaryDto
{
    public int PaymentMonth { get; set; }
    public decimal Principal { get; set; }
    public decimal Interest { get; set; }
    public decimal Balance { get; set; }
    public decimal TotalInterest { get; set; }
    public decimal TotalPayment { get; set; }
    public int LoanId { get; set; }
}
