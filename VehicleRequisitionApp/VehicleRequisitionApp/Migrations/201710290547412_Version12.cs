namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TblRequisitionDetails", "Place", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblRequisitionDetails", "Place", c => c.String());
        }
    }
}
