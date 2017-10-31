namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblRequestApprovalDetails", "RequisitionId", c => c.Int());
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId", c => c.Int(nullable: false));
            CreateIndex("dbo.TblRequestApprovalDetails", "ApprovalStatus");
            AddForeignKey("dbo.TblRequestApprovalDetails", "ApprovalStatus", "dbo.LookupUserGroups", "UserGroupsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblRequestApprovalDetails", "ApprovalStatus", "dbo.LookupUserGroups");
            DropIndex("dbo.TblRequestApprovalDetails", new[] { "ApprovalStatus" });
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId", c => c.Int());
            AlterColumn("dbo.TblRequestApprovalDetails", "RequisitionId", c => c.Int(nullable: false));
        }
    }
}
