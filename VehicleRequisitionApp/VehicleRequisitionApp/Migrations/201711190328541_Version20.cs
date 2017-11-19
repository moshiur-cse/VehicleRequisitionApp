namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LookupProjects", "ProjectName", c => c.String());
            AddColumn("dbo.LookupProjects", "ProjectPl", c => c.String());
            AddColumn("dbo.LookupProjects", "ProjectType", c => c.String());
            AddColumn("dbo.LookupProjects", "ProjectCluster", c => c.String());
            AddColumn("dbo.LookupProjects", "ProjectClient", c => c.String());
            AddColumn("dbo.LookupProjects", "Exclude", c => c.String());
            DropColumn("dbo.LookupProjects", "ProjectTitle");
            DropColumn("dbo.LookupProjects", "ClientName");
            DropColumn("dbo.LookupProjects", "ProjectStartingDate");
            DropColumn("dbo.LookupProjects", "ProjectEndingDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LookupProjects", "ProjectEndingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LookupProjects", "ProjectStartingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LookupProjects", "ClientName", c => c.String());
            AddColumn("dbo.LookupProjects", "ProjectTitle", c => c.String());
            DropColumn("dbo.LookupProjects", "Exclude");
            DropColumn("dbo.LookupProjects", "ProjectClient");
            DropColumn("dbo.LookupProjects", "ProjectCluster");
            DropColumn("dbo.LookupProjects", "ProjectType");
            DropColumn("dbo.LookupProjects", "ProjectPl");
            DropColumn("dbo.LookupProjects", "ProjectName");
        }
    }
}
