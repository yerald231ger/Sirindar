USE [sirindarDb]
GO

/****** Object:  Trigger [dbo].[TAMAsignacionBloques]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAMAspNetUsers]
	
	ON [dbo].[AspNetUsers]
	
	AFTER UPDATE 
AS 
BEGIN
	UPDATE AspNetUsers
    SET FechaModificacion = GETDATE()
    FROM inserted
	WHERE inserted.Id = AspNetUsers.Id
END

GO

/****** Object:  Trigger [dbo].[TAIAspNetUsers]    Script Date: 13/03/2016 14:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TAIAspNetUsers]
	
	ON [dbo].[AspNetUsers]
	
	AFTER INSERT 
AS 
BEGIN
	UPDATE AspNetUsers
    SET FechaAlta = GETDATE()
    FROM inserted
	WHERE inserted.Id = AspNetUsers.Id
END

GO


