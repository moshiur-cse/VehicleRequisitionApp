namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblRequisitionDetails", "AssignedDriverEmpId", c => c.Int());
            AlterColumn("dbo.TblRequisitionDetails", "AssignedVehicleId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblRequisitionDetails", "AssignedVehicleId", c => c.Int(nullable: false));
            AlterColumn("dbo.TblRequisitionDetails", "AssignedDriverEmpId", c => c.Int(nullable: false));
        }
    }
}
