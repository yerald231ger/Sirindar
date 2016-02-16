namespace CNSirindar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregardiassemana : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblCantidadComidas", "Lunes", c => c.Boolean(nullable: false));
            AddColumn("dbo.TblCantidadComidas", "Martes", c => c.Boolean(nullable: false));
            AddColumn("dbo.TblCantidadComidas", "Miercoles", c => c.Boolean(nullable: false));
            AddColumn("dbo.TblCantidadComidas", "Jueves", c => c.Boolean(nullable: false));
            AddColumn("dbo.TblCantidadComidas", "Viernes", c => c.Boolean(nullable: false));
            AddColumn("dbo.TblCantidadComidas", "Sabado", c => c.Boolean(nullable: false));
            AddColumn("dbo.TblCantidadComidas", "Domingo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TblCantidadComidas", "Domingo");
            DropColumn("dbo.TblCantidadComidas", "Sabado");
            DropColumn("dbo.TblCantidadComidas", "Viernes");
            DropColumn("dbo.TblCantidadComidas", "Jueves");
            DropColumn("dbo.TblCantidadComidas", "Miercoles");
            DropColumn("dbo.TblCantidadComidas", "Martes");
            DropColumn("dbo.TblCantidadComidas", "Lunes");
        }
    }
}
