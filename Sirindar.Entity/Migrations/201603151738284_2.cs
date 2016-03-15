namespace Sirindar.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TblDeportistas", "FechaRegistro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblDeportistas", "FechaRegistro", c => c.DateTime());
        }
    }
}
