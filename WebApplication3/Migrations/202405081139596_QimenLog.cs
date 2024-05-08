namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QimenLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QimenLogs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        BillNo = c.String(),
                        Data = c.String(),
                        Response = c.String(),
                        QueryString = c.String(),
                        HttpMethod = c.String(),
                        AddDTM = c.DateTime(nullable: false),
                        ModifyDTM = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QimenLogs");
        }
    }
}
