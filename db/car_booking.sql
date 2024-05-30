USE [car_booking]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/30/2024 2:44:32 PM ******/
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
/****** Object:  Table [dbo].[Booking]    Script Date: 5/30/2024 2:44:32 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingHistory]    Script Date: 5/30/2024 2:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[BookingId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_BookingHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 5/30/2024 2:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[Id] [uniqueidentifier] NOT NULL,
	[BienSoXe] [varchar](15) NOT NULL,
	[Phone] [varchar](15) NULL,
	[Avartar] [varchar](max) NULL,
	[TypeCar] [int] NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 5/30/2024 2:44:32 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeCar]    Script Date: 5/30/2024 2:44:32 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240527155701_UpdateTableBooking', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240528025421_UpdateTableDriver', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240528081406_AddPrimaryKey', N'3.1.0')
INSERT [dbo].[Booking] ([Id], [UserId], [DiemDon], [DiemDen], [Status], [NgayTao], [DriverId], [Phone], [Name], [UnitPrice]) VALUES (N'd7f3573f-9f4f-4c73-be03-473d5966417d', N'd60882f7-9272-4dc0-9b96-dee9453d3527', N'5 Hoa Sữa, quận Phú Nhuận, thành phố Hồ Chí Minh', N'Hẻm 416 Dương Quảng Hàm, quận Gò Vấp, thành phố Hồ Chí Minh', 4, CAST(N'2024-05-30T09:40:01.590' AS DateTime), N'bd4a849c-1ea2-4a07-8858-4621e9595fa7', N'0899963019', N'Nguyễn Bảo', CAST(38210 AS Decimal(18, 0)))
INSERT [dbo].[Booking] ([Id], [UserId], [DiemDon], [DiemDen], [Status], [NgayTao], [DriverId], [Phone], [Name], [UnitPrice]) VALUES (N'0f99416d-08d2-467b-a80f-be69d6f0e4a3', N'd60882f7-9272-4dc0-9b96-dee9453d3527', N'5 Hoa Sữa, quận Phú Nhuận, thành phố Hồ Chí Minh', N'Hẻm 116 Dương Quảng Hàm, quận Gò Vấp, thành phố Hồ Chí Minh', 4, CAST(N'2024-05-30T09:13:37.223' AS DateTime), N'bd4a849c-1ea2-4a07-8858-4621e9595fa7', N'0899963019', N'Nguyễn Bảo', CAST(35725 AS Decimal(18, 0)))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'940aebb9-14b8-492a-82a2-5344378cab5a', N'd7f3573f-9f4f-4c73-be03-473d5966417d', 4, CAST(N'2024-05-30T09:41:24.440' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'a3c650cb-e01d-44bb-81ab-5c1cde72c000', N'd7f3573f-9f4f-4c73-be03-473d5966417d', 2, CAST(N'2024-05-30T09:40:06.040' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'dc02a067-337d-446d-825c-5e5058aa2a1d', N'0f99416d-08d2-467b-a80f-be69d6f0e4a3', 1, CAST(N'2024-05-30T09:13:37.227' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'3f7a12d0-67e9-4bc0-8710-6c1c23615bcf', N'd7f3573f-9f4f-4c73-be03-473d5966417d', 3, CAST(N'2024-05-30T09:41:11.977' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'ce195221-9aef-49f4-ae6a-7ee8ddb13126', N'0f99416d-08d2-467b-a80f-be69d6f0e4a3', 3, CAST(N'2024-05-30T09:14:47.877' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'9429e6aa-0ee1-4e8b-9ae7-85b45645a16d', N'0f99416d-08d2-467b-a80f-be69d6f0e4a3', 4, CAST(N'2024-05-30T09:15:03.493' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'95cc9cac-ebdc-445f-b602-c08ee78594b8', N'0f99416d-08d2-467b-a80f-be69d6f0e4a3', 2, CAST(N'2024-05-30T09:13:41.107' AS DateTime))
INSERT [dbo].[BookingHistory] ([Id], [BookingId], [Status], [Time]) VALUES (N'a6864e27-711e-47c6-9597-dac5c9d2e461', N'd7f3573f-9f4f-4c73-be03-473d5966417d', 1, CAST(N'2024-05-30T09:40:01.590' AS DateTime))
INSERT [dbo].[Driver] ([Id], [BienSoXe], [Phone], [Avartar], [TypeCar], [Name]) VALUES (N'bd4a849c-1ea2-4a07-8858-4621e9595fa7', N'86C2-01670', N'0789123456', NULL, 1, N'Nguyễn Văn Bình')
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'Tìm tài xế')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Đang đón khách')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Đang trong chuyến')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (4, N'Hoàn thành')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (5, N'Hủy chuyến')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[TypeCar] ON 

INSERT [dbo].[TypeCar] ([Id], [Name], [GiaCuoc2KMDau], [GiaCuocSau2KM]) VALUES (1, N'Xe máy', CAST(12000 AS Decimal(18, 0)), CAST(3500 AS Decimal(18, 0)))
INSERT [dbo].[TypeCar] ([Id], [Name], [GiaCuoc2KMDau], [GiaCuocSau2KM]) VALUES (2, N'Xe ô tô 4 chỗ', CAST(29000 AS Decimal(18, 0)), CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[TypeCar] ([Id], [Name], [GiaCuoc2KMDau], [GiaCuocSau2KM]) VALUES (3, N'Xe ô tô 7 chỗ', CAST(34000 AS Decimal(18, 0)), CAST(13000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[TypeCar] OFF
