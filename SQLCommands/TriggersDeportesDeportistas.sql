USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMDeportesDeportistas]
	
	ON [dbo].[TblDeportesDeportistas]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblDeportesDeportistas
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.DeporteDeportistaId = TblDeportesDeportistas.DeporteDeportistaId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIDeportesDeportistas]
	
	ON [dbo].[TblDeportesDeportistas]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblDeportesDeportistas
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.DeporteDeportistaId = TblDeportesDeportistas.DeporteDeportistaId
END

GO



