namespace VehicleRequisitionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LookUpDivisions",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        DivShortName = c.String(),
                        DivFullName = c.String(),
                    })
                .PrimaryKey(t => t.DivisionId);
            
            CreateTable(
                "dbo.LookUpEmployees",
                c => new
                    {
                        EmpId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        EmpTypeId = c.Int(nullable: false),
                        EmpInitial = c.String(nullable: false, maxLength: 100, unicode: false),
                        EmpFullName = c.String(nullable: false, maxLength: 8000, unicode: false),
                        EmpDivisionId = c.Int(nullable: false),
                        EmpDesignation = c.String(nullable: false, maxLength: 8000, unicode: false),
                        EmpLevel = c.String(nullable: false, maxLength: 8000, unicode: false),
                        EmpEmail = c.String(nullable: false),
                        EmpMobile = c.String(),
                        EmpRoomNo = c.String(),
                        EmpPresentAddress = c.String(),
                        EmpNid = c.String(),
                        EmpPaasportNo = c.String(),
                        EmpBloodGroup = c.String(),
                        EmpHighestDegree = c.String(),
                        EmpHighestDegreeMajorSubject = c.String(),
                        EmpCareerSummary = c.String(),
                    })
                .PrimaryKey(t => t.EmpId)
                .ForeignKey("dbo.LookUpDivisions", t => t.EmpDivisionId, cascadeDelete: true)
                .ForeignKey("dbo.LookUpEmployeeTypes", t => t.EmpTypeId, cascadeDelete: true)
                .Index(t => t.EmpTypeId)
                .Index(t => t.EmpDivisionId);
            
            CreateTable(
                "dbo.LookUpEmployeeTypes",
                c => new
                    {
                        EmpTypeId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        EmpType = c.String(),
                    })
                .PrimaryKey(t => t.EmpTypeId);
            
            CreateTable(
                "dbo.LookupProjectLeaders",
                c => new
                    {
                        ProjectLeaderId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        EmpId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectLeaderId)
                .ForeignKey("dbo.LookUpEmployees", t => t.EmpId, cascadeDelete: true)
                .ForeignKey("dbo.LookupProjects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmpId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.LookupProjects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        ProjectCode = c.String(),
                        ProjectTitle = c.String(),
                        ClientName = c.String(),
                        ProjectStartingDate = c.DateTime(nullable: false),
                        ProjectEndingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.TblRequisitionDetails",
                c => new
                    {
                        RequisitionId = c.Int(nullable: false, identity: true),
                        RequisitionCategoryId = c.Int(nullable: false),
                        EmpId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        RequestSubmissionDate = c.DateTime(nullable: false),
                        RequiredFromDate = c.DateTime(nullable: false),
                        RequiredToDate = c.DateTime(nullable: false),
                        Place = c.String(),
                        Reason = c.String(),
                        ActuallyUsedFromDate = c.DateTime(nullable: false),
                        ActuallyUsedToDate = c.DateTime(nullable: false),
                        AssignedDriverEmpId = c.Int(nullable: false),
                        AssignedVehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionId)
                .ForeignKey("dbo.LookUpEmployees", t => t.EmpId, cascadeDelete: true)
                .ForeignKey("dbo.LookupProjects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.LookupRequisitionCategories", t => t.RequisitionCategoryId, cascadeDelete: true)
                .Index(t => t.RequisitionCategoryId)
                .Index(t => t.EmpId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.LookupRequisitionCategories",
                c => new
                    {
                        RequisitionCategoryId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        RequisitionCategory = c.String(),
                    })
                .PrimaryKey(t => t.RequisitionCategoryId);
            
            CreateTable(
                "dbo.TblDirectors",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        EmpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DirectorId)
                .ForeignKey("dbo.LookUpEmployees", t => t.EmpId, cascadeDelete: true)
                .Index(t => t.EmpId);
            
            CreateTable(
                "dbo.TblManagementDetails",
                c => new
                    {
                        ManagementDetailId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        ManagementTypeId = c.Int(nullable: false),
                        EmpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManagementDetailId)
                .ForeignKey("dbo.LookUpEmployees", t => t.EmpId, cascadeDelete: true)
                .ForeignKey("dbo.LookUpManagementTypes", t => t.ManagementTypeId, cascadeDelete: true)
                .Index(t => t.ManagementTypeId)
                .Index(t => t.EmpId);
            
            CreateTable(
                "dbo.LookUpManagementTypes",
                c => new
                    {
                        ManagementTypeId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        ManagementType = c.String(),
                    })
                .PrimaryKey(t => t.ManagementTypeId);
            
            CreateTable(
                "dbo.TblUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.LookUpEmployees", t => t.EmpId, cascadeDelete: true)
                .Index(t => t.EmpId);
            
            CreateTable(
                "dbo.TblUserGroupDistributions",
                c => new
                    {
                        UserGroupDistributionId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        UserGroupsId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupDistributionId)
                .ForeignKey("dbo.LookupUserGroups", t => t.UserGroupsId, cascadeDelete: true)
                .ForeignKey("dbo.TblUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserGroupsId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LookupUserGroups",
                c => new
                    {
                        UserGroupsId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        UserGroupName = c.String(),
                    })
                .PrimaryKey(t => t.UserGroupsId);
            
            CreateTable(
                "dbo.LookupVehicles",
                c => new
                    {
                        VehiclesId = c.Int(nullable: false, identity: true),
                        SortingSerialNo = c.Int(nullable: false),
                        VehicleNo = c.String(),
                        LicenseNo = c.String(),
                        VehicleModel = c.String(),
                    })
                .PrimaryKey(t => t.VehiclesId);
            
            CreateTable(
                "dbo.TblRequestApprovalDetails",
                c => new
                    {
                        RequestApprovalDetailId = c.Int(nullable: false, identity: true),
                        RequisitionId = c.Int(nullable: false),
                        ApprovalAuthorityId = c.Int(nullable: false),
                        ApprovalStatus = c.Boolean(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.RequestApprovalDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblUserGroupDistributions", "UserId", "dbo.TblUsers");
            DropForeignKey("dbo.TblUserGroupDistributions", "UserGroupsId", "dbo.LookupUserGroups");
            DropForeignKey("dbo.TblUsers", "EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.TblManagementDetails", "ManagementTypeId", "dbo.LookUpManagementTypes");
            DropForeignKey("dbo.TblManagementDetails", "EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.TblDirectors", "EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.TblRequisitionDetails", "RequisitionCategoryId", "dbo.LookupRequisitionCategories");
            DropForeignKey("dbo.TblRequisitionDetails", "ProjectId", "dbo.LookupProjects");
            DropForeignKey("dbo.TblRequisitionDetails", "EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.LookupProjectLeaders", "ProjectId", "dbo.LookupProjects");
            DropForeignKey("dbo.LookupProjectLeaders", "EmpId", "dbo.LookUpEmployees");
            DropForeignKey("dbo.LookUpEmployees", "EmpTypeId", "dbo.LookUpEmployeeTypes");
            DropForeignKey("dbo.LookUpEmployees", "EmpDivisionId", "dbo.LookUpDivisions");
            DropIndex("dbo.TblUserGroupDistributions", new[] { "UserId" });
            DropIndex("dbo.TblUserGroupDistributions", new[] { "UserGroupsId" });
            DropIndex("dbo.TblUsers", new[] { "EmpId" });
            DropIndex("dbo.TblManagementDetails", new[] { "EmpId" });
            DropIndex("dbo.TblManagementDetails", new[] { "ManagementTypeId" });
            DropIndex("dbo.TblDirectors", new[] { "EmpId" });
            DropIndex("dbo.TblRequisitionDetails", new[] { "ProjectId" });
            DropIndex("dbo.TblRequisitionDetails", new[] { "EmpId" });
            DropIndex("dbo.TblRequisitionDetails", new[] { "RequisitionCategoryId" });
            DropIndex("dbo.LookupProjectLeaders", new[] { "ProjectId" });
            DropIndex("dbo.LookupProjectLeaders", new[] { "EmpId" });
            DropIndex("dbo.LookUpEmployees", new[] { "EmpDivisionId" });
            DropIndex("dbo.LookUpEmployees", new[] { "EmpTypeId" });
            DropTable("dbo.TblRequestApprovalDetails");
            DropTable("dbo.LookupVehicles");
            DropTable("dbo.LookupUserGroups");
            DropTable("dbo.TblUserGroupDistributions");
            DropTable("dbo.TblUsers");
            DropTable("dbo.LookUpManagementTypes");
            DropTable("dbo.TblManagementDetails");
            DropTable("dbo.TblDirectors");
            DropTable("dbo.LookupRequisitionCategories");
            DropTable("dbo.TblRequisitionDetails");
            DropTable("dbo.LookupProjects");
            DropTable("dbo.LookupProjectLeaders");
            DropTable("dbo.LookUpEmployeeTypes");
            DropTable("dbo.LookUpEmployees");
            DropTable("dbo.LookUpDivisions");
        }
    }
}
