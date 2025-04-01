using LoanSimulator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanSimulator.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Loan> Loans { get; set; }
    public DbSet<PaymentFlowSummary> PaymentFlowSummarys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Loan>().ToTable("Proposta");
        modelBuilder.Entity<PaymentFlowSummary>().ToTable("PaymentFlowSummary");
    }
}