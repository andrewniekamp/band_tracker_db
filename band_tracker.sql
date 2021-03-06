USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 8/5/2016 3:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[performances]    Script Date: 8/5/2016 3:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[performances](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[venue_id] [int] NULL,
	[band_id] [int] NULL,
	[performance_date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 8/5/2016 3:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name]) VALUES (19, N'Shakira')
INSERT [dbo].[bands] ([id], [name]) VALUES (20, N'Band of Horses')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[performances] ON 

INSERT [dbo].[performances] ([id], [venue_id], [band_id], [performance_date]) VALUES (35, 12, 19, CAST(N'2016-08-18T00:00:00.000' AS DateTime))
INSERT [dbo].[performances] ([id], [venue_id], [band_id], [performance_date]) VALUES (36, 13, 20, CAST(N'2016-08-19T00:00:00.000' AS DateTime))
INSERT [dbo].[performances] ([id], [venue_id], [band_id], [performance_date]) VALUES (37, 13, 19, CAST(N'2016-08-02T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[performances] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name]) VALUES (12, N'The Fraze Pavillion')
INSERT [dbo].[venues] ([id], [name]) VALUES (13, N'Paramount Theatre')
SET IDENTITY_INSERT [dbo].[venues] OFF
