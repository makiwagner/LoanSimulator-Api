using LoanSimulator.Domain.Entities;

namespace LoanSimulator.Domain.Interfaces;

public interface ILoanRepository
{
    Task AddLoanAsync(Loan loan);
    Task<Loan> GetLoanByIdAsync(int id);
}