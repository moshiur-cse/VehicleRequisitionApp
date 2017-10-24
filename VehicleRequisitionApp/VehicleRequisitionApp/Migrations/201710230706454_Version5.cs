namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LookUpFileUploads",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        EmpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LookUpFileUploads");
        }
    }
}
