-- Verifica se o banco de dados já existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LoanSimulationDB')
BEGIN
    -- Cria o banco de dados
    CREATE DATABASE LoanSimulationDB;
END
GO