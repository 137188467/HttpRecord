namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class log : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QimenLogs", "MethodName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QimenLogs", "MethodName");
        }
    }
}
