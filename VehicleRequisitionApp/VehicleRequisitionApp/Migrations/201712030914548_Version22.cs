namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LookUpCombinedRequisitions",
                c => new
                    {
                        CombinedRequisitionId = c.Int(nullable: false, identity: true),
                        RequisitionId = c.Int(nullable: false),
                        AssignId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CombinedRequisitionId)
                .ForeignKey("dbo.TblRequisitionDetails", t => t.RequisitionId, cascadeDelete: true)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LookUpCombinedRequisitions", "RequisitionId", "dbo.TblRequisitionDetails");
            DropIndex("dbo.LookUpCombinedRequisitions", new[] { "RequisitionId" });
            DropTable("dbo.LookUpCombinedRequisitions");
        }
    }
}
