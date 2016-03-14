USE [sirindarDb]
GO
/****** Object:  Trigger [dbo].[TADAsignacionBloques]    Script Date: 13/03/2016 14:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TAIAsignacionBloques]
	
	ON [dbo].[TblAsignacionBloques]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblAsignacionBloques
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.AsignacionBloqueId = TblAsignacionBloques.AsignacionBloqueId
END

/****** Object:  Trigger [dbo].[TAMAsignacionBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMAsignacionBloques]
	
	ON [dbo].[TblAsignacionBloques]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblAsignacionBloques
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.AsignacionBloqueId = TblAsignacionBloques.AsignacionBloqueId
END

GO


