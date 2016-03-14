USE [sirindarDb]
GO

/****** Object:  Table [dbo].[TblBloques]    Script Date: 13/03/2016 14:41:49 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMDependencias]
	
	ON [dbo].[TblDependencias]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE TblDependencias
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.DependenciaId = TblDependencias.DependenciaId
END

GO

/****** Object:  Trigger [dbo].[TAIBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIDependencias]
	
	ON [dbo].[TblDependencias]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE TblDependencias
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.DependenciaId = TblDependencias.DependenciaId
END

GO



