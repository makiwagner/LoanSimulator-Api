using LoanSimulator.Domain.Entities;

namespace LoanSimulator.Domain.Services;

public class LoanService
{
    public Loan CalculateLoan(decimal loanAmount, decimal annualInterestRate, int numberOfMonths)
    {
        var monthlyInterestRate = annualInterestRate / 12;

        var monthlyPayment = (loanAmount * monthlyInterestRate) /
            (1 - (decimal)Math.Pow((double)(1 + monthlyInterestRate), -numberOfMonths));

        var loan = new Loan
        {
            LoanAmount = loanAmount,
            AnnualInterestRate = annualInterestRate,
            NumberOfMonths = numberOfMonths
        };

        decimal balance = loanAmount;

        for (int month = 1; month <= numberOfMonths; month++)
        {
            var interest = balance * monthlyInterestRate;
            var principal = monthlyPayment - interest;
            balance -= principal;

            loan.PaymentFlowSummary.Add(new PaymentFlowSummary
            {
                PaymentMonth = month,
                Principal = principal,
                Interest = interest,
                Balance = Math.Max(balance, 0)
            });
        }

        return loan;
    }
}