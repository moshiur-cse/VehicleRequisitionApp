namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version19 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TblRequisitionDetails", "StateId");
            AddForeignKey("dbo.TblRequisitionDetails", "StateId", "dbo.LookUpStates", "StateId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblRequisitionDetails", "StateId", "dbo.LookUpStates");
            DropIndex("dbo.TblRequisitionDetails", new[] { "StateId" });
        }
    }
}
