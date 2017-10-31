namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LookUpStates",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            AddColumn("dbo.TblRequisitionDetails", "StateId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TblRequisitionDetails", "StateId");
            DropTable("dbo.LookUpStates");
        }
    }
}
