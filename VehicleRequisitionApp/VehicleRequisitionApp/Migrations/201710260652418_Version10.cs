namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblDrivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.LookUpEmployees", t => t.EmpId, cascadeDelete: true)
                .Index(t => t.EmpId);
            
            CreateIndex("dbo.TblRequisitionDetails", "AssignedVehicleId");
            AddForeignKey("dbo.TblRequisitionDetails", "AssignedVehicleId", "dbo.LookupVehicles", "VehiclesId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblDrivers", "EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.TblRequisitionDetails", "AssignedVehicleId", "dbo.LookupVehicles");
            DropIndex("dbo.TblDrivers", new[] { "EmpId" });
            DropIndex("dbo.TblRequisitionDetails", new[] { "AssignedVehicleId" });
            DropTable("dbo.TblDrivers");
        }
    }
}
