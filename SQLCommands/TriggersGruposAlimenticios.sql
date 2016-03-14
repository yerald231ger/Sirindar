USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMGruposAlimenticios]
	
	ON [dbo].[TblGruposAlimenticios]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblGruposAlimenticios
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.GrupoAlimenticioId = TblGruposAlimenticios.GrupoAlimenticioId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIGruposAlimenticios]
	
	ON [dbo].[TblGruposAlimenticios]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblGruposAlimenticios
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.GrupoAlimenticioId = TblGruposAlimenticios.GrupoAlimenticioId
END

GO



