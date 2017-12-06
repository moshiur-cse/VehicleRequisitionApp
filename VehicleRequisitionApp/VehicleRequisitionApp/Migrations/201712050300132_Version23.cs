namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblRequisitionDetails", "Assignd", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TblRequisitionDetails", "Assignd");
        }
    }
}
