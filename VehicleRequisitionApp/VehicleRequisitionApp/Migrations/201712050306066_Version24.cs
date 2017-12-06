namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblRequisitionDetails", "AssigndId", c => c.Int());
            DropColumn("dbo.TblRequisitionDetails", "Assignd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblRequisitionDetails", "Assignd", c => c.Int());
            DropColumn("dbo.TblRequisitionDetails", "AssigndId");
        }
    }
}
