namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LookUpEmployees", "EmpPinNo", c => c.String());
            AlterColumn("dbo.LookUpEmployees", "SortingSerialNo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LookUpEmployees", "SortingSerialNo", c => c.Int(nullable: false));
            DropColumn("dbo.LookUpEmployees", "EmpPinNo");
        }
    }
}
