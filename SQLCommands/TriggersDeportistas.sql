USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMDeportistas]
	
	ON [dbo].[TblDeportistas]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblDeportistas
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.DeportistaId = TblDeportistas.DeportistaId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIDeportistas]
	
	ON [dbo].[TblDeportistas]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblDeportistas
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.DeportistaId = TblDeportistas.DeportistaId
END

GO





