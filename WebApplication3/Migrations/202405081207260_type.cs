namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QimenLogs", "AddDTM", c => c.DateTime());
            AlterColumn("dbo.QimenLogs", "ModifyDTM", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QimenLogs", "ModifyDTM", c => c.DateTime(nullable: false));
            AlterColumn("dbo.QimenLogs", "AddDTM", c => c.DateTime(nullable: false));
        }
    }
}
