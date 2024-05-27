USE [car_booking]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 27/05/2024 11:48:44 CH ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 27/05/2024 11:48:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [varchar](50) NULL,
	[DiemDon] [nvarchar](max) NOT NULL,
	[DiemDen] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[NgayTao] [datetime] NOT NULL,
	[DriverId] [varchar](50) NULL,
	[Phone] [varchar](15) NULL,
	[Name] [nvarchar](100) NULL,
	[UnitPrice] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingHistory]    Script Date: 27/05/2024 11:48:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[BookingId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Time] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 27/05/2024 11:48:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[Id] [varchar](50) NULL,
	[BienSoXe] [varchar](15) NOT NULL,
	[Phone] [varchar](15) NULL,
	[Avartar] [varchar](max) NULL,
	[TypeCar] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 27/05/2024 11:48:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeCar]    Script Date: 27/05/2024 11:48:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeCar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[GiaCuoc2KMDau] [decimal](18, 0) NULL,
	[GiaCuocSau2KM] [decimal](18, 0) NULL,
 CONSTRAINT [PK_TypeCar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240527155701_UpdateTableBooking', N'3.1.0')
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'Tìm tài xế')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Đang đón khách')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Đang trong chuyến')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (4, N'Hoàn thành')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (5, N'Hủy chuyến')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeCar] ON 

INSERT [dbo].[TypeCar] ([Id], [Name], [GiaCuoc2KMDau], [GiaCuocSau2KM]) VALUES (1, N'Xe máy', CAST(12000 AS Decimal(18, 0)), CAST(3500 AS Decimal(18, 0)))
INSERT [dbo].[TypeCar] ([Id], [Name], [GiaCuoc2KMDau], [GiaCuocSau2KM]) VALUES (2, N'Xe ô tô 4 chỗ', CAST(29000 AS Decimal(18, 0)), CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[TypeCar] ([Id], [Name], [GiaCuoc2KMDau], [GiaCuocSau2KM]) VALUES (3, N'Xe ô tô 7 chỗ', CAST(34000 AS Decimal(18, 0)), CAST(13000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[TypeCar] OFF
GO
