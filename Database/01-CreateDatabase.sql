-- ============================================================
-- Kurd Studio Database Creation Script
-- SQL Server LocalDB
-- ============================================================

USE master;
GO

-- Drop database if exists (for development purposes)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'KurdStudioDb')
BEGIN
    ALTER DATABASE KurdStudioDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE KurdStudioDb;
END
GO

-- Create the database
CREATE DATABASE KurdStudioDb
ON PRIMARY (
    NAME = KurdStudioDb_Data,
    FILENAME = 'C:\Users\Public\KurdStudioDb.mdf',
    SIZE = 50MB,
    MAXSIZE = 500MB,
    FILEGROWTH = 10MB
)
LOG ON (
    NAME = KurdStudioDb_Log,
    FILENAME = 'C:\Users\Public\KurdStudioDb_Log.ldf',
    SIZE = 10MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
);
GO

-- Set recovery model to simple for development
ALTER DATABASE KurdStudioDb SET RECOVERY SIMPLE;
GO

-- Use the database
USE KurdStudioDb;
GO

PRINT 'KurdStudioDb database created successfully.';
GO
