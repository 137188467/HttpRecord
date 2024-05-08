namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statustype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QimenLogs", "BillType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QimenLogs", "BillType");
        }
    }
}
