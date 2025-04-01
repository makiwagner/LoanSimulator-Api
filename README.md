# Sistema de Simulação de Empréstimos com Tabela Price

API RESTFul utilizando .NET 9.

Aplicação disponível no endereço: https://localhost:5002/swagger/index.html


## Estrutura do projeto

```
LoanSimulation.API/
│src/
├── Domain/
│   ├── Entities/
│   │   ├── Loan.cs
│   │   └── PaymentFlowSummary.cs
│   ├── Interfaces/
│   │   └── ILoanRepository.cs
│   └── Services/
│       └── LoanService.cs
├── Application/
│   ├── DTOs/
│   │   ├── LoanRequestDTO.cs
│   │   ├── LoanResponseDTO.cs
│   │   └── PaymentFlowSummaryDto.cs
│   ├── Mappings/
│   │   └── LoanProfile.cs
│   └── Services/
│       └── LoanApplicationService.cs
├── Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   └── Repository/
│       └── LoanRepository.cs
│   
├── CrossCutting/
│   └── DependencyInjection/
│       └── DependencyInjection.cs
├── Api/
│   └── Controllers/
│       └── LoanController.cs
│
└ tests
  └── UnitTests/
      └── LoanSimulator.UnitTests/
	      └── LoanApplicationServiceTests.cs
```