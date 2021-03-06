USE [goldconf]
GO
/****** Object:  Table [dbo].[CompraUsuario]    Script Date: 02/05/2021 12:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompraUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdConferencia] [int] NULL,
	[IdUser] [int] NULL,
 CONSTRAINT [PK_Comprar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Conferencia]    Script Date: 02/05/2021 12:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conferencia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImagePath] [nvarchar](100) NULL,
	[PonenteId] [int] NULL,
	[FechaConf] [datetime] NULL,
	[TituloConf] [nvarchar](100) NULL,
	[PrecioConf] [decimal](18, 5) NULL,
	[DetalleConf] [nvarchar](500) NULL,
	[Ap1] [nvarchar](500) NULL,
	[Ap2] [nvarchar](500) NULL,
	[Ap3] [nvarchar](500) NULL,
	[Definicion] [nvarchar](500) NULL,
	[Objetivo] [nvarchar](500) NULL,
	[MetaFinal] [nvarchar](500) NULL,
 CONSTRAINT [PK_Conferencia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 02/05/2021 12:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NULL,
	[Currency] [nvarchar](100) NULL,
	[Amount] [decimal](18, 4) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ponente]    Script Date: 02/05/2021 12:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ponente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [nvarchar](200) NULL,
	[NomApe] [nvarchar](300) NULL,
	[Especialidad] [nvarchar](300) NULL,
	[Email] [nvarchar](300) NULL,
	[Telefono] [nvarchar](300) NULL,
	[Logros] [nvarchar](200) NULL,
	[Experiencia] [nvarchar](300) NULL,
 CONSTRAINT [PK_Ponente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 02/05/2021 12:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuentaId] [int] NULL,
	[Tipo] [nvarchar](200) NULL,
	[FechaHora] [datetime] NULL,
	[Motivo] [nvarchar](100) NULL,
	[Amount] [decimal](18, 4) NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 02/05/2021 12:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomApe] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CompraUsuario]  WITH CHECK ADD  CONSTRAINT [FK_CompraUsuario_Conferencia] FOREIGN KEY([IdConferencia])
REFERENCES [dbo].[Conferencia] ([Id])
GO
ALTER TABLE [dbo].[CompraUsuario] CHECK CONSTRAINT [FK_CompraUsuario_Conferencia]
GO
ALTER TABLE [dbo].[CompraUsuario]  WITH CHECK ADD  CONSTRAINT [FK_CompraUsuario_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CompraUsuario] CHECK CONSTRAINT [FK_CompraUsuario_User]
GO
ALTER TABLE [dbo].[Conferencia]  WITH CHECK ADD  CONSTRAINT [FK_Conferencia_Ponente] FOREIGN KEY([PonenteId])
REFERENCES [dbo].[Ponente] ([Id])
GO
ALTER TABLE [dbo].[Conferencia] CHECK CONSTRAINT [FK_Conferencia_Ponente]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_User]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Cuenta]
GO
