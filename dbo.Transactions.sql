USE [CustomerInquiryDb]
GO

/****** Object: Table [dbo].[Transactions] Script Date: 4/14/2019 10:30:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions] (
    [TransactionID]       INT             IDENTITY (1, 1) NOT NULL,
    [TransactionDateTime] DATETIME2 (7)   NOT NULL,
    [Amount]              DECIMAL (18, 2) NOT NULL,
    [CurrencyCode]        NVARCHAR (3)    NULL,
    [Status]              INT             NOT NULL,
    [CustomerId]          INT             NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Transactions_CustomerId]
    ON [dbo].[Transactions]([CustomerId] ASC);


GO
ALTER TABLE [dbo].[Transactions]
    ADD CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([TransactionID] ASC);


GO
ALTER TABLE [dbo].[Transactions]
    ADD CONSTRAINT [FK_Transactions_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerID]) ON DELETE CASCADE;


