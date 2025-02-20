USE [master]
GO
/****** Object:  Database [Microsoft.eShopOnWeb.CatalogDb]    Script Date: 7/1/2023 7:12:13 AM ******/
CREATE DATABASE [Microsoft.eShopOnWeb.CatalogDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Microsoft.eShopOnWeb.CatalogDb', FILENAME = N'C:\Users\User\Microsoft.eShopOnWeb.CatalogDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Microsoft.eShopOnWeb.CatalogDb_log', FILENAME = N'C:\Users\User\Microsoft.eShopOnWeb.CatalogDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Microsoft.eShopOnWeb.CatalogDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET  MULTI_USER 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET QUERY_STORE = OFF
GO
USE [Microsoft.eShopOnWeb.CatalogDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Microsoft.eShopOnWeb.CatalogDb]
GO
USE [Microsoft.eShopOnWeb.CatalogDb]
GO
/****** Object:  Sequence [dbo].[catalog_brand_hilo]    Script Date: 7/1/2023 7:12:13 AM ******/
CREATE SEQUENCE [dbo].[catalog_brand_hilo] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 10
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [Microsoft.eShopOnWeb.CatalogDb]
GO
/****** Object:  Sequence [dbo].[catalog_hilo]    Script Date: 7/1/2023 7:12:13 AM ******/
CREATE SEQUENCE [dbo].[catalog_hilo] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 10
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [Microsoft.eShopOnWeb.CatalogDb]
GO
/****** Object:  Sequence [dbo].[catalog_type_hilo]    Script Date: 7/1/2023 7:12:13 AM ******/
CREATE SEQUENCE [dbo].[catalog_type_hilo] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 10
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
/****** Object:  Table [dbo].[CatalogBrands]    Script Date: 7/1/2023 7:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogBrands](
	[Id] [int] NOT NULL,
	[Brand] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CatalogBrands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatalogTypes]    Script Date: 7/1/2023 7:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogTypes](
	[Id] [int] NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CatalogTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 7/1/2023 7:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PictureUri] [nvarchar](max) NULL,
	[CatalogTypeId] [int] NOT NULL,
	[CatalogBrandId] [int] NOT NULL,
 CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[CatalogWithBrandAndTypeView]    Script Date: 7/1/2023 7:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CatalogWithBrandAndTypeView]
AS
SELECT        dbo.Catalog.Id, dbo.Catalog.Name, dbo.Catalog.Description, dbo.Catalog.Price, dbo.Catalog.PictureUri, dbo.Catalog.CatalogTypeId, dbo.Catalog.CatalogBrandId, dbo.CatalogBrands.Brand AS CatalogBrand, 
                         dbo.CatalogTypes.Type AS CatalogType
FROM            dbo.Catalog INNER JOIN
                         dbo.CatalogBrands ON dbo.Catalog.CatalogBrandId = dbo.CatalogBrands.Id INNER JOIN
                         dbo.CatalogTypes ON dbo.Catalog.CatalogTypeId = dbo.CatalogTypes.Id
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/1/2023 7:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasketItems]    Script Date: 7/1/2023 7:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasketItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CatalogItemId] [int] NOT NULL,
	[BasketId] [int] NOT NULL,
 CONSTRAINT [PK_BasketItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Baskets]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Baskets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuyerId] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Baskets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemOrdered_CatalogItemId] [int] NULL,
	[ItemOrdered_ProductName] [nvarchar](50) NULL,
	[ItemOrdered_PictureUri] [nvarchar](max) NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Units] [int] NOT NULL,
	[OrderId] [int] NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuyerId] [nvarchar](256) NOT NULL,
	[OrderDate] [datetimeoffset](7) NOT NULL,
	[ShipToAddress_Street] [nvarchar](180) NOT NULL,
	[ShipToAddress_City] [nvarchar](100) NOT NULL,
	[ShipToAddress_State] [nvarchar](60) NULL,
	[ShipToAddress_Country] [nvarchar](90) NOT NULL,
	[ShipToAddress_ZipCode] [nvarchar](18) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20201202111507_InitialModel', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211026175614_FixBuyerId', N'7.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211231093753_FixShipToAddress', N'7.0.5')
INSERT [dbo].[CatalogBrands] ([Id], [Brand]) VALUES (1, N'Azure')
INSERT [dbo].[CatalogBrands] ([Id], [Brand]) VALUES (2, N'.NET')
INSERT [dbo].[CatalogBrands] ([Id], [Brand]) VALUES (3, N'Visual Studio')
INSERT [dbo].[CatalogBrands] ([Id], [Brand]) VALUES (4, N'SQL Server')
INSERT [dbo].[CatalogBrands] ([Id], [Brand]) VALUES (5, N'Other')
INSERT [dbo].[CatalogTypes] ([Id], [Type]) VALUES (1, N'Mug')
INSERT [dbo].[CatalogTypes] ([Id], [Type]) VALUES (2, N'T-Shirt')
INSERT [dbo].[CatalogTypes] ([Id], [Type]) VALUES (3, N'Sheet')
INSERT [dbo].[CatalogTypes] ([Id], [Type]) VALUES (4, N'USB Memory Stick')
/****** Object:  Index [IX_BasketItems_BasketId]    Script Date: 7/1/2023 7:12:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_BasketItems_BasketId] ON [dbo].[BasketItems]
(
	[BasketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Catalog]    Script Date: 7/1/2023 7:12:14 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Catalog] ON [dbo].[Catalog]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Catalog_CatalogBrandId]    Script Date: 7/1/2023 7:12:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_Catalog_CatalogBrandId] ON [dbo].[Catalog]
(
	[CatalogBrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Catalog_CatalogTypeId]    Script Date: 7/1/2023 7:12:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_Catalog_CatalogTypeId] ON [dbo].[Catalog]
(
	[CatalogTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_OrderId]    Script Date: 7/1/2023 7:12:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [BuyerId]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [ShipToAddress_Street]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [ShipToAddress_City]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [ShipToAddress_Country]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [ShipToAddress_ZipCode]
GO
ALTER TABLE [dbo].[BasketItems]  WITH CHECK ADD  CONSTRAINT [FK_BasketItems_Baskets_BasketId] FOREIGN KEY([BasketId])
REFERENCES [dbo].[Baskets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BasketItems] CHECK CONSTRAINT [FK_BasketItems_Baskets_BasketId]
GO
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD  CONSTRAINT [FK_Catalog_CatalogBrands_CatalogBrandId] FOREIGN KEY([CatalogBrandId])
REFERENCES [dbo].[CatalogBrands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Catalog] CHECK CONSTRAINT [FK_Catalog_CatalogBrands_CatalogBrandId]
GO
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD  CONSTRAINT [FK_Catalog_CatalogTypes_CatalogTypeId] FOREIGN KEY([CatalogTypeId])
REFERENCES [dbo].[CatalogTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Catalog] CHECK CONSTRAINT [FK_Catalog_CatalogTypes_CatalogTypeId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
/****** Object:  StoredProcedure [dbo].[AddBasket]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddBasket] 
	-- Add the parameters for the stored procedure here
	@BuyerId NVARCHAR(256),
	@InsertedId INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 INSERT INTO Baskets (BuyerId) VALUES(@BuyerId)
	 SET @InsertedId = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[AddCatalogItem]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCatalogItem]
    @Name NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @PictureUri NVARCHAR(MAX),
    @CatalogTypeId INT,
    @CatalogBrandId INT,
	@InsertedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Catalog (Name, Description, Price, PictureUri, CatalogTypeId, CatalogBrandId)
    VALUES (@Name, @Description, @Price, @PictureUri, @CatalogTypeId, @CatalogBrandId);
	SET @InsertedId = SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[AddItemToBasket]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddItemToBasket] 
	-- Add the parameters for the stored procedure here
	@BasketId INT,
	@UnitPrice DECIMAL,
	@Quantity INT,
	@CatalogItemId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[BasketItems]
           ([UnitPrice]
           ,[Quantity]
           ,[CatalogItemId]
           ,[BasketId])
     VALUES
           (@UnitPrice
           ,@Quantity
           ,@CatalogItemId
           ,@BasketId)
END
GO
/****** Object:  StoredProcedure [dbo].[AddNewOrder]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewOrder]
    @BuyerId nvarchar(256),
    @OrderDate datetimeoffset(7),
    @ShipToAddress_Street nvarchar(180),
    @ShipToAddress_City nvarchar(100),
    @ShipToAddress_State nvarchar(60) = NULL,
    @ShipToAddress_Country nvarchar(90),
    @ShipToAddress_ZipCode nvarchar(18),
	@InsertedId INT OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Orders] (
        [BuyerId],
        [OrderDate],
        [ShipToAddress_Street],
        [ShipToAddress_City],
        [ShipToAddress_State],
        [ShipToAddress_Country],
        [ShipToAddress_ZipCode]
    )
    VALUES (
        @BuyerId,
        @OrderDate,
        @ShipToAddress_Street,
        @ShipToAddress_City,
        @ShipToAddress_State,
        @ShipToAddress_Country,
        @ShipToAddress_ZipCode
    )
	SET @InsertedId = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[AddNewOrderItem]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewOrderItem]
    @ItemOrdered_CatalogItemId INT,
    @ItemOrdered_ProductName NVARCHAR(50),
    @ItemOrdered_PictureUri NVARCHAR(MAX),
    @UnitPrice DECIMAL(18, 2),
    @Units INT,
    @OrderId INT,
    @InsertedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[OrderItems] (ItemOrdered_CatalogItemId, ItemOrdered_ProductName, ItemOrdered_PictureUri, UnitPrice, Units, OrderId)
    VALUES (@ItemOrdered_CatalogItemId, @ItemOrdered_ProductName, @ItemOrdered_PictureUri, @UnitPrice, @Units, @OrderId);
	SET @InsertedId = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllItemsFromBasket]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAllItemsFromBasket]
	-- Add the parameters for the stored procedure here
	@BasketId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    DELETE FROM BasketItems WHERE BasketId = @BasketId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteBasket]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteBasket]
	-- Add the parameters for the stored procedure here
@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  DELETE FROM Baskets WHERE ID = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCatalogItem]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCatalogItem] 
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Catalog WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[FetchBasketById]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FetchBasketById]
	-- Add the parameters for the stored procedure here
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[BuyerId]
	FROM [dbo].[Baskets] WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[FetchBasketItems]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FetchBasketItems] 
	-- Add the parameters for the stored procedure here
	@BasketId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[UnitPrice]
      ,[Quantity]
      ,[CatalogItemId]
      ,[BasketId]
  FROM [dbo].[BasketItems] WHERE BasketId = @BasketId
END
GO
/****** Object:  StoredProcedure [dbo].[FetchBuyerBasket]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FetchBuyerBasket]
	-- Add the parameters for the stored procedure here
	@BuyerId NVARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id,BuyerId from Baskets WHERE BuyerId = @BuyerId order by Id
END
GO
/****** Object:  StoredProcedure [dbo].[FetchCatalogItems]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FetchCatalogItems] 
	@PageNumber INT = null,
    @PageSize INT = null,
	@CatalogTypeId INT = null,
	@CatalogBrandId INT = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @OffsetRows INT;
	
	DECLARE @SQL nvarchar(1000);
	DECLARE @SQLWHERE nvarchar(100);
	DECLARE @OffsetClaus nvarchar(100);

	IF(@PageNumber IS NOT NULL AND @PageSize IS NOT NULL AND @PageSize <> 0)
	BEGIN
		SET @OffsetRows = (@PageNumber) * @PageSize;
		SET @OffsetClaus = ' OFFSET ' + CAST(@OffsetRows AS VARCHAR(10)) + ' ROWS
		FETCH NEXT ' + CAST(@PageSize AS VARCHAR(10)) + ' ROWS ONLY';
	END

	SET @SQL = 'SELECT [Id]
      ,[Name]
      ,[Description]
      ,[Price]
      ,[PictureUri]
      ,[CatalogTypeId]
      ,[CatalogBrandId]
      ,[CatalogBrand]
      ,[CatalogType] from CatalogWithBrandAndTypeView';

	IF(@CatalogTypeId IS NOT null)
	BEGIN
	 SELECT @SQLWHERE = ' WHERE CatalogTypeId = ' + CAST(@CatalogTypeId AS VARCHAR(10))
	END
	ELSE
	BEGIN
	 SELECT @SQLWHERE = ' WHERE CatalogTypeId IS NOT NULL'
	END

	IF(@CatalogBrandId IS NOT null)
	BEGIN
	 SELECT @SQLWHERE = ' AND CatalogBrandId = ' + CAST(@CatalogTypeId AS VARCHAR(10))
	END
	
	
	SELECT @SQL = CONCAT(@SQL,@SQLWHERE,' ORDER BY (SELECT 1) '); 
	SELECT @SQL = CONCAT(@SQL, @OffsetClaus);
	
	PRINT @SQL

	EXEC (@SQL)
END
GO
/****** Object:  StoredProcedure [dbo].[ResetDatabase]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ResetDatabase]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

ALTER TABLE dbo.OrderItems
	DROP CONSTRAINT FK_OrderItems_Orders_OrderId

ALTER TABLE [dbo].[BasketItems] DROP CONSTRAINT [FK_BasketItems_Baskets_BasketId]


TRUNCATE TABLE OrderItems
TRUNCATE TABLE Orders
TRUNCATE TABLE BasketItems
TRUNCATE TABLE Baskets
TRUNCATE TABLE Catalog

ALTER TABLE [dbo].[OrderItems]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])


ALTER TABLE [dbo].[BasketItems]  WITH CHECK ADD  CONSTRAINT [FK_BasketItems_Baskets_BasketId] FOREIGN KEY([BasketId])
REFERENCES [dbo].[Baskets] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[BasketItems] CHECK CONSTRAINT [FK_BasketItems_Baskets_BasketId]


END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCatalogItem]    Script Date: 7/1/2023 7:12:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCatalogItem]
    @Id INT,
    @Name NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @PictureUri NVARCHAR(MAX),
    @CatalogTypeId INT,
    @CatalogBrandId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Catalog
    SET Name = @Name,
        Description = @Description,
        Price = @Price,
        PictureUri = @PictureUri,
        CatalogTypeId = @CatalogTypeId,
        CatalogBrandId = @CatalogBrandId
    WHERE Id = @Id;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Catalog"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 224
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CatalogBrands"
            Begin Extent = 
               Top = 151
               Left = 448
               Bottom = 373
               Right = 618
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CatalogTypes"
            Begin Extent = 
               Top = 13
               Left = 338
               Bottom = 139
               Right = 508
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CatalogWithBrandAndTypeView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CatalogWithBrandAndTypeView'
GO
USE [master]
GO
ALTER DATABASE [Microsoft.eShopOnWeb.CatalogDb] SET  READ_WRITE 
GO
