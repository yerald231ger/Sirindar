namespace Sirindar.Entity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblAsignacionBloques",
                c => new
                    {
                        AsignacionBloqueId = c.Int(nullable: false, identity: true),
                        DeportistaId = c.Int(nullable: false),
                        DeporteId = c.Int(nullable: false),
                        BloqueId = c.Int(),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AsignacionBloqueId)
                .ForeignKey("dbo.TblBloques", t => t.BloqueId)
                .ForeignKey("dbo.TblDeportes", t => t.DeporteId, cascadeDelete: true)
                .ForeignKey("dbo.TblDeportistas", t => t.DeportistaId, cascadeDelete: true)
                .Index(t => t.DeportistaId)
                .Index(t => t.DeporteId)
                .Index(t => t.BloqueId);
            
            CreateTable(
                "dbo.TblBloques",
                c => new
                    {
                        BloqueId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        KilocaloriasTotales = c.Int(),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BloqueId);
            
            CreateTable(
                "dbo.TblGrupos",
                c => new
                    {
                        GrupoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Porcentaje = c.Int(nullable: false),
                        Gramos = c.Int(nullable: false),
                        Kilocalorias = c.Int(nullable: false),
                        Equivalencias = c.String(nullable: false, maxLength: 50),
                        BloqueId = c.Int(nullable: false),
                        GrupoAlimenticioId = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GrupoId)
                .ForeignKey("dbo.TblGruposAlimenticios", t => t.GrupoAlimenticioId, cascadeDelete: true)
                .ForeignKey("dbo.TblBloques", t => t.BloqueId, cascadeDelete: true)
                .Index(t => t.BloqueId)
                .Index(t => t.GrupoAlimenticioId);
            
            CreateTable(
                "dbo.TblGruposAlimenticios",
                c => new
                    {
                        GrupoAlimenticioId = c.Int(nullable: false, identity: true),
                        Grupo = c.String(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GrupoAlimenticioId);
            
            CreateTable(
                "dbo.TblDeportes",
                c => new
                    {
                        DeporteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50),
                        TipoEnergia = c.Int(nullable: false),
                        ClasificacionDeporteId = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeporteId)
                .ForeignKey("dbo.TblClasificacionDeportes", t => t.ClasificacionDeporteId, cascadeDelete: true)
                .Index(t => t.ClasificacionDeporteId);
            
            CreateTable(
                "dbo.TblClasificacionDeportes",
                c => new
                    {
                        ClasificacionDeporteId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 50),
                        Abreviatura = c.String(nullable: false, maxLength: 5),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClasificacionDeporteId);
            
            CreateTable(
                "dbo.TblDeportesDeportistas",
                c => new
                    {
                        DeporteDeportistaId = c.Int(nullable: false, identity: true),
                        DeportistaId = c.Int(nullable: false),
                        DeporteId = c.Int(nullable: false),
                        IniciaEntrenamiento = c.Time(precision: 7),
                        FinalizaEntrenamiento = c.Time(precision: 7),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeporteDeportistaId)
                .ForeignKey("dbo.TblDeportes", t => t.DeporteId, cascadeDelete: true)
                .ForeignKey("dbo.TblDeportistas", t => t.DeportistaId, cascadeDelete: true)
                .Index(t => t.DeportistaId)
                .Index(t => t.DeporteId);
            
            CreateTable(
                "dbo.TblDeportistas",
                c => new
                    {
                        DeportistaId = c.Int(nullable: false, identity: true),
                        DependenciaId = c.Int(nullable: false),
                        Matricula = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false, storeType: "date"),
                        Genero = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeportistaId)
                .ForeignKey("dbo.TblDependencias", t => t.DependenciaId, cascadeDelete: true)
                .Index(t => t.DependenciaId);
            
            CreateTable(
                "dbo.TblAsistencias",
                c => new
                    {
                        AsistenciaId = c.Int(nullable: false, identity: true),
                        HoraAsistencia = c.DateTime(nullable: false),
                        DeportistaId = c.Int(nullable: false),
                        HorarioId = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AsistenciaId)
                .ForeignKey("dbo.TblDeportistas", t => t.DeportistaId, cascadeDelete: true)
                .ForeignKey("dbo.TblHorarios", t => t.HorarioId, cascadeDelete: true)
                .Index(t => t.DeportistaId)
                .Index(t => t.HorarioId);
            
            CreateTable(
                "dbo.TblHorarios",
                c => new
                    {
                        HorarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.Int(nullable: false),
                        Inicia = c.Time(nullable: false, precision: 7),
                        Finaliza = c.Time(nullable: false, precision: 7),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HorarioId);
            
            CreateTable(
                "dbo.TblDependencias",
                c => new
                    {
                        DependenciaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Clave = c.String(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DependenciaId);
            
            CreateTable(
                "dbo.TblEntrenadores",
                c => new
                    {
                        EntrenadorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false, storeType: "date"),
                        Genero = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EntrenadorId);
            
            CreateTable(
                "dbo.TblHorarioComidas",
                c => new
                    {
                        DeportistaId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Lunes = c.Boolean(nullable: false),
                        Martes = c.Boolean(nullable: false),
                        Miercoles = c.Boolean(nullable: false),
                        Jueves = c.Boolean(nullable: false),
                        Viernes = c.Boolean(nullable: false),
                        Sabado = c.Boolean(nullable: false),
                        Domingo = c.Boolean(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeportistaId)
                .ForeignKey("dbo.TblDeportistas", t => t.DeportistaId)
                .Index(t => t.DeportistaId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        Telefono = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        FechaAlta = c.DateTime(),
                        FechaModificacion = c.DateTime(),
                        EsActivo = c.Boolean(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EntrenadorDependencias",
                c => new
                    {
                        Entrenador_EntrenadorId = c.Int(nullable: false),
                        Dependencia_DependenciaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Entrenador_EntrenadorId, t.Dependencia_DependenciaId })
                .ForeignKey("dbo.TblEntrenadores", t => t.Entrenador_EntrenadorId, cascadeDelete: true)
                .ForeignKey("dbo.TblDependencias", t => t.Dependencia_DependenciaId, cascadeDelete: true)
                .Index(t => t.Entrenador_EntrenadorId)
                .Index(t => t.Dependencia_DependenciaId);
            
            CreateTable(
                "dbo.EntrenadorDeportes",
                c => new
                    {
                        Entrenador_EntrenadorId = c.Int(nullable: false),
                        Deporte_DeporteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Entrenador_EntrenadorId, t.Deporte_DeporteId })
                .ForeignKey("dbo.TblEntrenadores", t => t.Entrenador_EntrenadorId, cascadeDelete: true)
                .ForeignKey("dbo.TblDeportes", t => t.Deporte_DeporteId, cascadeDelete: true)
                .Index(t => t.Entrenador_EntrenadorId)
                .Index(t => t.Deporte_DeporteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TblAsignacionBloques", "DeportistaId", "dbo.TblDeportistas");
            DropForeignKey("dbo.TblAsignacionBloques", "DeporteId", "dbo.TblDeportes");
            DropForeignKey("dbo.TblDeportesDeportistas", "DeportistaId", "dbo.TblDeportistas");
            DropForeignKey("dbo.TblHorarioComidas", "DeportistaId", "dbo.TblDeportistas");
            DropForeignKey("dbo.EntrenadorDeportes", "Deporte_DeporteId", "dbo.TblDeportes");
            DropForeignKey("dbo.EntrenadorDeportes", "Entrenador_EntrenadorId", "dbo.TblEntrenadores");
            DropForeignKey("dbo.EntrenadorDependencias", "Dependencia_DependenciaId", "dbo.TblDependencias");
            DropForeignKey("dbo.EntrenadorDependencias", "Entrenador_EntrenadorId", "dbo.TblEntrenadores");
            DropForeignKey("dbo.TblDeportistas", "DependenciaId", "dbo.TblDependencias");
            DropForeignKey("dbo.TblAsistencias", "HorarioId", "dbo.TblHorarios");
            DropForeignKey("dbo.TblAsistencias", "DeportistaId", "dbo.TblDeportistas");
            DropForeignKey("dbo.TblDeportesDeportistas", "DeporteId", "dbo.TblDeportes");
            DropForeignKey("dbo.TblDeportes", "ClasificacionDeporteId", "dbo.TblClasificacionDeportes");
            DropForeignKey("dbo.TblAsignacionBloques", "BloqueId", "dbo.TblBloques");
            DropForeignKey("dbo.TblGrupos", "BloqueId", "dbo.TblBloques");
            DropForeignKey("dbo.TblGrupos", "GrupoAlimenticioId", "dbo.TblGruposAlimenticios");
            DropIndex("dbo.EntrenadorDeportes", new[] { "Deporte_DeporteId" });
            DropIndex("dbo.EntrenadorDeportes", new[] { "Entrenador_EntrenadorId" });
            DropIndex("dbo.EntrenadorDependencias", new[] { "Dependencia_DependenciaId" });
            DropIndex("dbo.EntrenadorDependencias", new[] { "Entrenador_EntrenadorId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TblHorarioComidas", new[] { "DeportistaId" });
            DropIndex("dbo.TblAsistencias", new[] { "HorarioId" });
            DropIndex("dbo.TblAsistencias", new[] { "DeportistaId" });
            DropIndex("dbo.TblDeportistas", new[] { "DependenciaId" });
            DropIndex("dbo.TblDeportesDeportistas", new[] { "DeporteId" });
            DropIndex("dbo.TblDeportesDeportistas", new[] { "DeportistaId" });
            DropIndex("dbo.TblDeportes", new[] { "ClasificacionDeporteId" });
            DropIndex("dbo.TblGrupos", new[] { "GrupoAlimenticioId" });
            DropIndex("dbo.TblGrupos", new[] { "BloqueId" });
            DropIndex("dbo.TblAsignacionBloques", new[] { "BloqueId" });
            DropIndex("dbo.TblAsignacionBloques", new[] { "DeporteId" });
            DropIndex("dbo.TblAsignacionBloques", new[] { "DeportistaId" });
            DropTable("dbo.EntrenadorDeportes");
            DropTable("dbo.EntrenadorDependencias");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TblHorarioComidas");
            DropTable("dbo.TblEntrenadores");
            DropTable("dbo.TblDependencias");
            DropTable("dbo.TblHorarios");
            DropTable("dbo.TblAsistencias");
            DropTable("dbo.TblDeportistas");
            DropTable("dbo.TblDeportesDeportistas");
            DropTable("dbo.TblClasificacionDeportes");
            DropTable("dbo.TblDeportes");
            DropTable("dbo.TblGruposAlimenticios");
            DropTable("dbo.TblGrupos");
            DropTable("dbo.TblBloques");
            DropTable("dbo.TblAsignacionBloques");
        }
    }
}
