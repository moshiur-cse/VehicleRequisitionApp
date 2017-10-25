namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoopUpRedirectPages",
                c => new
                    {
                        RedirectPageId = c.Int(nullable: false, identity: true),
                        UserGroupsId = c.Int(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        ParameterName = c.String(),
                    })
                .PrimaryKey(t => t.RedirectPageId);
            
            AlterColumn("dbo.LookUpEmployees", "EmpPinNo", c => c.String(maxLength: 50));
            CreateIndex("dbo.LookUpEmployees", "EmpPinNo", unique: true, name: "PinNo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LookUpEmployees", "PinNo");
            AlterColumn("dbo.LookUpEmployees", "EmpPinNo", c => c.String());
            DropTable("dbo.LoopUpRedirectPages");
        }
    }
}
