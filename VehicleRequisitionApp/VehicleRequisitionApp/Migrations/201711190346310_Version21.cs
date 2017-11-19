namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LookupProjects", "SortingSerialNo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LookupProjects", "SortingSerialNo", c => c.Int(nullable: false));
        }
    }
}
