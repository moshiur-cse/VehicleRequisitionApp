namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId", c => c.Int());
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalStatus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TblRequestApprovalDetails", "ApprovalAuthorityId", c => c.Int(nullable: false));
        }
    }
}
