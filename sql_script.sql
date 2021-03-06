USE [StudEvidencija]
GO
/****** Object:  Table [dbo].[EvidencijaRada]    Script Date: 20.12.2021. 22:36:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EvidencijaRada](
	[StudID] [int] NOT NULL,
	[Datum] [date] NOT NULL,
	[VrijemeOd] [time](0) NULL,
	[VrijemeDo] [time](0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 20.12.2021. 22:36:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudID] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[Fakultet] [nvarchar](50) NOT NULL,
	[Aktivan] [bit] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[EvidencijaRada_Insert]    Script Date: 20.12.2021. 22:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EvidencijaRada_Insert] (@StudID int, @Datum date, @VrijemeOd time, @VrijemeDo time)
  AS 
  		BEGIN
			INSERT INTO dbo.EvidencijaRada (StudID, Datum, VrijemeOd, VrijemeDo)
			VALUES (@StudID, @Datum, @VrijemeOd, @VrijemeDo)
		END
GO
/****** Object:  StoredProcedure [dbo].[stp_Master]    Script Date: 20.12.2021. 22:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_Master] (@StudID int=null, @Ime varchar(50), @Prezime varchar(50),  @Fakultet varchar(50), @Aktivan bit, @Statement nvarchar(20))
  AS 
	BEGIN
		IF @Statement='Insert'
			BEGIN
				INSERT INTO dbo.Student (Ime, Prezime, Fakultet, Aktivan)
				VALUES (@Ime, @Prezime, @Fakultet, @Aktivan)
			END
		IF @Statement='Select'
			BEGIN
				SELECT StudID, Ime, Prezime, Fakultet
				FROM dbo.Student
			END
		IF @Statement='Update'
			BEGIN
				UPDATE dbo.Student
				SET Aktivan=@Aktivan
				WHERE StudID=@StudID
			END
		IF @Statement='Delete'
			BEGIN
				DELETE FROM dbo.Student
				WHERE StudID=@StudID
			END
	END
	 
GO
