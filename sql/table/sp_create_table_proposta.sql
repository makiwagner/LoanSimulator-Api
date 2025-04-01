USE LoanSimulationDB;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Loan')
BEGIN
CREATE TABLE Loan 
(
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    LoanAmount          DECIMAL(18, 2) NOT NULL,
    AnnualInterestRate  DECIMAL(5, 4) NOT NULL,
    NumberOfMonths      INT NOT NULL
);
END
GO