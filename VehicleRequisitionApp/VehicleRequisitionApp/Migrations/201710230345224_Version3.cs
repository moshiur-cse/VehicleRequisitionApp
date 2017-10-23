namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblRequisitionDetails", "ActuallyUsedFromDate", c => c.DateTime());
            AlterColumn("dbo.TblRequisitionDetails", "ActuallyUsedToDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblRequisitionDetails", "ActuallyUsedToDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TblRequisitionDetails", "ActuallyUsedFromDate", c => c.DateTime(nullable: false));
        }
    }
}
