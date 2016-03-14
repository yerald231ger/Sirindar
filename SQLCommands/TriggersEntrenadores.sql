USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMEntrenadores]
	
	ON [dbo].[TblEntrenadores]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblEntrenadores
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.EntrenadorId = TblEntrenadores.EntrenadorId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIEntrenadores]
	
	ON [dbo].[TblEntrenadores]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblEntrenadores
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.EntrenadorId = TblEntrenadores.EntrenadorId
END

GO




