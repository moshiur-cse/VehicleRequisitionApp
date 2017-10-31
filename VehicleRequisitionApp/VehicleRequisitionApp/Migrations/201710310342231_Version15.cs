namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version15 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId");
            AddForeignKey("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId", "dbo.LookUpEmployees", "EmpId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId", "dbo.LookUpEmployees");
            DropIndex("dbo.TblRequestApprovalDetails", new[] { "ApprovalAuthorityId" });
        }
    }
}
