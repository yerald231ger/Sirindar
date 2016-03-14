USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMClasificacionDeportes]
	
	ON [dbo].[TblClasificacionDeportes]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblClasificacionDeportes
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.ClasificacionDeporteId = TblClasificacionDeportes.ClasificacionDeporteId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIClasificacionDeportes]
	
	ON [dbo].[TblClasificacionDeportes]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblClasificacionDeportes
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.ClasificacionDeporteId = TblClasificacionDeportes.ClasificacionDeporteId
END

GO




