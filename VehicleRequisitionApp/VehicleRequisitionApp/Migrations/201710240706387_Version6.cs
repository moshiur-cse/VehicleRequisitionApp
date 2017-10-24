namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LookupRequisitionCategories", "SortingSerialNo");
            DropColumn("dbo.TblDirectors", "SortingSerialNo");
            DropColumn("dbo.TblUserGroupDistributions", "SortingSerialNo");
            DropColumn("dbo.LookupUserGroups", "SortingSerialNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LookupUserGroups", "SortingSerialNo", c => c.Int(nullable: false));
            AddColumn("dbo.TblUserGroupDistributions", "SortingSerialNo", c => c.Int(nullable: false));
            AddColumn("dbo.TblDirectors", "SortingSerialNo", c => c.Int(nullable: false));
            AddColumn("dbo.LookupRequisitionCategories", "SortingSerialNo", c => c.Int(nullable: false));
        }
    }
}
