USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMBloques]
	
	ON [dbo].[TblBloques]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblBloques
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.BloqueId = TblBloques.BloqueId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIBloques]
	
	ON [dbo].[TblBloques]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblBloques
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.BloqueId = TblBloques.BloqueId
END

GO


