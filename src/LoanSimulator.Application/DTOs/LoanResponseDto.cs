namespace LoanSimulator.Application.DTOs;

public class LoanResponseDto
{
    public decimal MonthlyPayment { get; set; }
    public decimal TotalInterest { get; set; }
    public decimal TotalPayment { get; set; }
    public List<PaymentFlowSummaryDto>? PaymentFlowSummary { get; set; }
}
