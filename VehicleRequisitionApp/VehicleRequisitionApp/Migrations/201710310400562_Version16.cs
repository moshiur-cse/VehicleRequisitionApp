namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblRequestApprovalDetails", "RequisitionId", c => c.Int(nullable: false));
            CreateIndex("dbo.TblRequestApprovalDetails", "RequisitionId");
            AddForeignKey("dbo.TblRequestApprovalDetails", "RequisitionId", "dbo.TblRequisitionDetails", "RequisitionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblRequestApprovalDetails", "RequisitionId", "dbo.TblRequisitionDetails");
            DropIndex("dbo.TblRequestApprovalDetails", new[] { "RequisitionId" });
            AlterColumn("dbo.TblRequestApprovalDetails", "RequisitionId", c => c.Int());
        }
    }
}
