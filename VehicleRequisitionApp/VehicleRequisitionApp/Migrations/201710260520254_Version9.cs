namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblRequisitionDetails", "UsedFromKM", c => c.Double());
            AddColumn("dbo.TblRequisitionDetails", "UsedToKM", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TblRequisitionDetails", "UsedToKM");
            DropColumn("dbo.TblRequisitionDetails", "UsedFromKM");
        }
    }
}
