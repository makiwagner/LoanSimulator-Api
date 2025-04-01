using LoanSimulator.Domain.Entities;
using LoanSimulator.Domain.Interfaces;
using LoanSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LoanSimulator.Infrastructure.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddLoanAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task<Loan> GetLoanByIdAsync(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.PaymentFlowSummary)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (loan is null)
            throw new KeyNotFoundException($"Loan with ID {id} not found");

        return loan;
    }
}