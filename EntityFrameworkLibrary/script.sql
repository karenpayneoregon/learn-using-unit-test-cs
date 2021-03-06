USE [DateTimeDatabase]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 6/27/2021 6:11:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleDate] [datetime2](7) NULL,
	[ShipCountry] [int] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([Id], [SaleDate], [ShipCountry]) VALUES (1, CAST(N'2021-06-17T05:01:53.3166667' AS DateTime2), 1)
INSERT [dbo].[Sales] ([Id], [SaleDate], [ShipCountry]) VALUES (2, CAST(N'2021-06-17T05:01:56.9800000' AS DateTime2), 4)
INSERT [dbo].[Sales] ([Id], [SaleDate], [ShipCountry]) VALUES (3, CAST(N'2021-06-27T05:01:59.4100000' AS DateTime2), 1)
INSERT [dbo].[Sales] ([Id], [SaleDate], [ShipCountry]) VALUES (4, CAST(N'2021-06-27T05:02:01.6800000' AS DateTime2), 5)
INSERT [dbo].[Sales] ([Id], [SaleDate], [ShipCountry]) VALUES (5, CAST(N'2021-06-27T05:17:22.6266667' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Sales] OFF
ALTER TABLE [dbo].[Sales] ADD  CONSTRAINT [DF_Sales_SaleDate]  DEFAULT (getdate()) FOR [SaleDate]
GO
