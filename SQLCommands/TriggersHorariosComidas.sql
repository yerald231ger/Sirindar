USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMHorarioComidas]
	
	ON [dbo].[TblHorarioComidas]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblHorarioComidas
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.DeportistaId = TblHorarioComidas.DeportistaId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIHorarioComidas]
	
	ON [dbo].[TblHorarioComidas]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblHorarioComidas
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.DeportistaId = TblHorarioComidas.DeportistaId
END

GO
