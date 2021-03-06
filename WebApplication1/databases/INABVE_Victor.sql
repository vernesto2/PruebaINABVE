USE [INABVE]
GO
/****** Object:  Table [dbo].[Beneficio]    Script Date: 26/1/2022 10:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beneficio](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](255) NULL,
	[FechaCreado] [datetime] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Beneficio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BeneficiosVeteranos]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeneficiosVeteranos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdVeterano] [bigint] NULL,
	[IdBeneficio] [bigint] NULL,
	[FechaCreado] [datetime] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_BeneficiosVeteranos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Veterano]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veterano](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DUI] [nvarchar](10) NULL,
	[Carnet] [nvarchar](25) NULL,
	[PrimerNombre] [nvarchar](25) NULL,
	[SegundoNombre] [nvarchar](25) NULL,
	[PrimerApellido] [nvarchar](25) NULL,
	[SegundoApellido] [nvarchar](25) NULL,
	[FechaCreado] [datetime] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Veterano] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Beneficio] ADD  CONSTRAINT [DF_Beneficio_FechaCreado]  DEFAULT (((1900)-(1))-(1)) FOR [FechaCreado]
GO
ALTER TABLE [dbo].[Beneficio] ADD  CONSTRAINT [DF_Beneficio_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[BeneficiosVeteranos] ADD  CONSTRAINT [DF_BeneficiosVeteranos_FechaCreado]  DEFAULT (((1900)-(1))-(1)) FOR [FechaCreado]
GO
ALTER TABLE [dbo].[BeneficiosVeteranos] ADD  CONSTRAINT [DF_BeneficiosVeteranos_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Veterano] ADD  CONSTRAINT [DF_Veterano_FechaCreado]  DEFAULT (((1900)-(1))-(1)) FOR [FechaCreado]
GO
ALTER TABLE [dbo].[Veterano] ADD  CONSTRAINT [DF_Veterano_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[BeneficiosVeteranos]  WITH CHECK ADD  CONSTRAINT [FK_BeneficiosVeteranos_Beneficio] FOREIGN KEY([IdBeneficio])
REFERENCES [dbo].[Beneficio] ([Id])
GO
ALTER TABLE [dbo].[BeneficiosVeteranos] CHECK CONSTRAINT [FK_BeneficiosVeteranos_Beneficio]
GO
ALTER TABLE [dbo].[BeneficiosVeteranos]  WITH CHECK ADD  CONSTRAINT [FK_BeneficiosVeteranos_Veterano] FOREIGN KEY([IdVeterano])
REFERENCES [dbo].[Veterano] ([Id])
GO
ALTER TABLE [dbo].[BeneficiosVeteranos] CHECK CONSTRAINT [FK_BeneficiosVeteranos_Veterano]
GO
/****** Object:  StoredProcedure [dbo].[spBeneficio_Actualizar]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBeneficio_Actualizar]
	@Id bigint,
	@Nombre nvarchar(50),
	@Descripcion nvarchar(255),
	@FechaCreado datetime,
	@Activo bit

as

update Beneficio set Nombre = @Nombre, Descripcion = @Descripcion, FechaCreado = @FechaCreado, Activo = @Activo where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[spBeneficio_Insertar]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBeneficio_Insertar]
	@Nombre nvarchar(50),
	@Descripcion nvarchar(255),
	@FechaCreado datetime,
	@Activo bit

as

insert into Beneficio(Nombre, Descripcion, FechaCreado, Activo)
	values(@Nombre, @Descripcion, @FechaCreado, @Activo)

select 
	b.Id,
	b.Nombre,
	b.Descripcion,
	b.FechaCreado,
	b.Activo

from Beneficio b
where b.Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[spBeneficio_ListarActivos]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spBeneficio_ListarActivos]
as

select 
	b.Id,
	b.Nombre,
	b.Descripcion,
	b.FechaCreado,
	b.Activo

from Beneficio b
where b.Activo = 1
GO
/****** Object:  StoredProcedure [dbo].[spBeneficio_ListarBeneficiosPorVeterano]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spBeneficio_ListarBeneficiosPorVeterano]
	@IdVeterano bigint
as

--declare @IdVeterano bigint = 7

select distinct
	b.Id,
	b.Nombre,
	b.Descripcion,
	b.FechaCreado,
	b.Activo

from Beneficio b
inner join BeneficiosVeteranos bv on b.Id = bv.IdBeneficio
where b.Activo = 1 and b.Id not in (select bv.IdBeneficio from BeneficiosVeteranos bv where bv.IdVeterano = @IdVeterano and bv.Activo = 1)
GO
/****** Object:  StoredProcedure [dbo].[spBeneficio_ObtenerPorId]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBeneficio_ObtenerPorId]
	@Id bigint
as

select 
	b.Id,
	b.Nombre,
	b.Descripcion,
	b.FechaCreado,
	b.Activo

from Beneficio b
where b.Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[spBeneficiosVeteranos_Actualizar]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBeneficiosVeteranos_Actualizar]
	@Id bigint,
	@IdBeneficio bigint,
	@IdVeterano bigint,
	@FechaCreado datetime,
	@Activo bit

as

update BeneficiosVeteranos set IdBeneficio = @IdBeneficio, IdVeterano= @IdVeterano, FechaCreado = @FechaCreado, Activo = @Activo where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[spBeneficiosVeteranos_Insertar]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spBeneficiosVeteranos_Insertar]
	@IdBeneficio bigint,
	@IdVeterano bigint,
	@FechaCreado datetime,
	@Activo bit

as


insert into BeneficiosVeteranos(IdBeneficio, IdVeterano, FechaCreado, Activo)
	values(@IdBeneficio, @IdVeterano, @FechaCreado, @Activo)

select 
	bv.Id,
	bv.IdBeneficio,
	bv.IdVeterano,
	bv.FechaCreado,
	bv.Activo

from BeneficiosVeteranos bv
where bv.Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[spBeneficiosVeteranos_ListarActivos]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spBeneficiosVeteranos_ListarActivos]
as

select 
	bv.Id,
	bv.IdBeneficio,
	bv.IdVeterano,
	bv.FechaCreado,
	bv.Activo

from BeneficiosVeteranos bv
where bv.Activo = 1
GO
/****** Object:  StoredProcedure [dbo].[spBeneficiosVeteranos_ObtenerPorId]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBeneficiosVeteranos_ObtenerPorId]
	@Id bigint
as

select 
	bv.Id,
	bv.IdBeneficio,
	bv.IdVeterano,
	bv.FechaCreado,
	bv.Activo

from BeneficiosVeteranos bv
where bv.Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[spBeneficiosVeteranos_ObtenerPorVeterano]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spBeneficiosVeteranos_ObtenerPorVeterano]
	@IdVeterano bigint
as
--declare @IdVeterano bigint = 3
select 
	bv.Id,
	bv.IdBeneficio,
	bv.IdVeterano,
	bv.FechaCreado,
	bv.Activo,
	b.Nombre

from BeneficiosVeteranos bv
inner join Beneficio b on bv.IdBeneficio = b.Id
where bv.IdVeterano = @IdVeterano and bv.Activo = 1 and b.Activo = 1
GO
/****** Object:  StoredProcedure [dbo].[spVeterano_Actualizar]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spVeterano_Actualizar]
	@Id bigint,
	@DUI nvarchar(10),
	@Carnet nvarchar(25),
	@PrimerNombre nvarchar(25),
	@SegundoNombre nvarchar(25),
	@PrimerApellido nvarchar(25),
	@SegundoApellido nvarchar(25),
	@FechaCreado datetime,
	@Activo bit

as

update Veterano set DUI = @DUI, Carnet = @Carnet, PrimerNombre = @PrimerNombre, SegundoNombre = @SegundoNombre,
					PrimerApellido = @PrimerApellido, SegundoApellido = @SegundoApellido, FechaCreado = @FechaCreado,
					Activo = @Activo where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[spVeterano_Insertar]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spVeterano_Insertar]
	@DUI nvarchar(10),
	@Carnet nvarchar(25),
	@PrimerNombre nvarchar(25),
	@SegundoNombre nvarchar(25),
	@PrimerApellido nvarchar(25),
	@SegundoApellido nvarchar(25),
	@FechaCreado datetime,
	@Activo bit

as

insert into Veterano(DUI, Carnet, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, FechaCreado, Activo)
	values(@DUI, @Carnet, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoNombre, @FechaCreado, @Activo)

select 
	v.Id,
	v.DUI,
	v.Carnet,
	v.PrimerNombre,
	v.SegundoNombre,
	v.PrimerApellido,
	v.SegundoApellido,
	v.FechaCreado,
	v.Activo

from Veterano v
where v.Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[spVeterano_ListarActivos]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spVeterano_ListarActivos]
as

select 
	v.Id,
	v.DUI,
	v.Carnet,
	v.PrimerNombre,
	v.SegundoNombre,
	v.PrimerApellido,
	v.SegundoApellido,
	v.FechaCreado,
	v.Activo

from Veterano v
where v.Activo = 1
GO
/****** Object:  StoredProcedure [dbo].[spVeterano_ObtenerPorId]    Script Date: 26/1/2022 10:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spVeterano_ObtenerPorId]
	@Id bigint
as

select 
	v.Id,
	v.DUI,
	v.Carnet,
	v.PrimerNombre,
	v.SegundoNombre,
	v.PrimerApellido,
	v.SegundoApellido,
	v.FechaCreado,
	v.Activo

from Veterano v
where v.Id = @Id
GO
