USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMHorarios]
	
	ON [dbo].[TblHorarios]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblHorarios
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.HorarioId = TblHorarios.HorarioId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIHorarios]
	
	ON [dbo].[TblHorarios]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblHorarios
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.HorarioId = TblHorarios.HorarioId
END

GO
