namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TblRequestApprovalDetails", "ApprovalStatus", "dbo.LookupUserGroups");
            DropIndex("dbo.TblRequestApprovalDetails", new[] { "ApprovalStatus" });
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalStatus", c => c.Int());
            CreateIndex("dbo.TblRequestApprovalDetails", "ApprovalStatus");
            AddForeignKey("dbo.TblRequestApprovalDetails", "ApprovalStatus", "dbo.LookupUserGroups", "UserGroupsId");
        }
    }
}
