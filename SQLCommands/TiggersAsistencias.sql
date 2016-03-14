USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblAsistencias]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMAsistencias]
	
	ON [dbo].[TblAsistencias]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblAsistencias
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.AsistenciaId = TblAsistencias.AsistenciaId
END

GO

/****** Object:  Trigger [dbo].[TAIAspNetUsers]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIAsistencias]
	
	ON [dbo].[TblAsistencias]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblAsistencias
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.AsistenciaId = TblAsistencias.AsistenciaId
END

GO


