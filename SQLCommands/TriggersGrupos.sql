USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET DENTIFIER ON
GO


Create TRIGGER [dbo].[TAMGrupos]
	
	ON [dbo].[TblGrupos]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblGrupos
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.GrupoId = TblGrupos.GrupoId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIGrupos]
	
	ON [dbo].[TblGrupos]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblGrupos
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.GrupoId = TblGrupos.GrupoId
END

GO




