USE [CustomerInquiryDb]
GO

/****** Object: Table [dbo].[Customers] Script Date: 4/14/2019 10:29:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers] (
    [CustomerID]   INT            IDENTITY (1, 1) NOT NULL,
    [CustomerName] NVARCHAR (30)  NOT NULL,
    [ContactEmail] NVARCHAR (MAX) NULL,
    [MobileNo]     NVARCHAR (10)  NOT NULL
);


