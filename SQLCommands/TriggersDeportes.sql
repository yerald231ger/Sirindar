USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMDeportes]
	
	ON [dbo].[TblDeportes]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblDeportes
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.DeporteId = TblDeportes.DeporteId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIDeportes]
	
	ON [dbo].[TblDeportes]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblDeportes
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.DeporteId = TblDeportes.DeporteId
END

GO



