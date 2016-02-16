namespace Sirindar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregartableconventions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FechaAlta", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "FechaModificacion", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "EsActivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EsActivo");
            DropColumn("dbo.AspNetUsers", "FechaModificacion");
            DropColumn("dbo.AspNetUsers", "FechaAlta");
        }
    }
}
