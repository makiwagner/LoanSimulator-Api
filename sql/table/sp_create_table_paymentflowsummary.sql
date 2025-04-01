USE LoanSimulationDB;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PaymentFlowSummary')
BEGIN
    CREATE TABLE PaymentFlowSummary 
    (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        PaymentMonth    INT NOT NULL,
        Principal       DECIMAL(18, 2) NOT NULL,
        Interest        DECIMAL(18, 2) NOT NULL,
        Balance         DECIMAL(18, 2) NOT NULL,
        TotalInterest   DECIMAL(18, 2) NOT NULL,
        TotalPayment    DECIMAL(18, 2) NOT NULL,
        LoanId          INT NOT NULL,
        FOREIGN KEY (LoanId) REFERENCES Loan(Id)
    );
END
GO