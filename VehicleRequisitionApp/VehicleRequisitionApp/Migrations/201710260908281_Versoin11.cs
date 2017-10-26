namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versoin11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TblRequisitionDetails", "EmpId", "dbo.LookUpEmployees");
            AddColumn("dbo.TblRequisitionDetails", "LookUpEmployee_EmpId", c => c.Int());
            CreateIndex("dbo.TblRequisitionDetails", "AssignedDriverEmpId");
            CreateIndex("dbo.TblRequisitionDetails", "LookUpEmployee_EmpId");
            AddForeignKey("dbo.TblRequisitionDetails", "AssignedDriverEmpId", "dbo.LookUpEmployees", "EmpId");
            AddForeignKey("dbo.TblRequisitionDetails", "LookUpEmployee_EmpId", "dbo.LookUpEmployees", "EmpId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblRequisitionDetails", "LookUpEmployee_EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.TblRequisitionDetails", "AssignedDriverEmpId", "dbo.LookUpEmployees");
            DropIndex("dbo.TblRequisitionDetails", new[] { "LookUpEmployee_EmpId" });
            DropIndex("dbo.TblRequisitionDetails", new[] { "AssignedDriverEmpId" });
            DropColumn("dbo.TblRequisitionDetails", "LookUpEmployee_EmpId");
            AddForeignKey("dbo.TblRequisitionDetails", "EmpId", "dbo.LookUpEmployees", "EmpId", cascadeDelete: true);
        }
    }
}
