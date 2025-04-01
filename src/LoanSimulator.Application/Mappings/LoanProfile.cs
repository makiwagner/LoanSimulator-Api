using AutoMapper;
using LoanSimulator.Application.DTOs;
using LoanSimulator.Domain.Entities;

namespace LoanSimulator.Application.Mappings;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, LoanResponseDto>().ReverseMap();
        CreateMap<PaymentFlowSummary, PaymentFlowSummaryDto>();
    }
}
