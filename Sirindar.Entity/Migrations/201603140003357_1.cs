namespace Sirindar.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblAsignacionBloques", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblAsignacionBloques", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblBloques", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblBloques", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblGrupos", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblGrupos", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblGruposAlimenticios", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblGruposAlimenticios", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblDeportes", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblDeportes", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblClasificacionDeportes", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblClasificacionDeportes", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblDeportesDeportistas", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblDeportesDeportistas", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblDeportistas", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblDeportistas", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblAsistencias", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblAsistencias", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblHorarios", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblHorarios", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblDependencias", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblDependencias", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblEntrenadores", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblEntrenadores", "FechaModificacion", c => c.DateTime());
            AlterColumn("dbo.TblHorarioComidas", "FechaAlta", c => c.DateTime());
            AlterColumn("dbo.TblHorarioComidas", "FechaModificacion", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblHorarioComidas", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblHorarioComidas", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblEntrenadores", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblEntrenadores", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDependencias", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDependencias", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblHorarios", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblHorarios", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblAsistencias", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblAsistencias", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDeportistas", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDeportistas", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDeportesDeportistas", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDeportesDeportistas", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblClasificacionDeportes", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblClasificacionDeportes", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDeportes", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblDeportes", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblGruposAlimenticios", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblGruposAlimenticios", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblGrupos", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblGrupos", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblBloques", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblBloques", "FechaAlta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblAsignacionBloques", "FechaModificacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblAsignacionBloques", "FechaAlta", c => c.DateTime(nullable: false));
        }
    }
}
