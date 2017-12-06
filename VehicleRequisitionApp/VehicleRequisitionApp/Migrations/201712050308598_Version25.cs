namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblRequisitionDetails", "AssignId", c => c.Int());
            DropColumn("dbo.TblRequisitionDetails", "AssigndId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblRequisitionDetails", "AssigndId", c => c.Int());
            DropColumn("dbo.TblRequisitionDetails", "AssignId");
        }
    }
}
