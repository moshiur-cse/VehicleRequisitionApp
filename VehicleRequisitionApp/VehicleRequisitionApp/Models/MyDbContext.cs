using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class MyDbContext:DbContext
    {
        public DbSet<TblUser> TblUsers { get; set; }
        public DbSet<TblManagementDetail> TblManagementDetails { get; set; }
        public DbSet<LookUpEmployee> LookUpEmployees { get; set; }
        public DbSet<LookUpEmployeeType> LookUpEmployeeTypes { get; set; }
        public DbSet<LookUpDivision> LookUpDivisions { get; set; }
        public DbSet<LookUpManagementType> LookUpManagementTypes { get; set; }
        public DbSet<TblDirector> TblDirectors { get; set; }
        public DbSet<LookupProject> LookupProjects { get; set; }
        public DbSet<LookupProjectLeader> LookupProjectLeaders { get; set; }

        public DbSet<TblRequisitionDetail> TblRequisitionDetails { get; set; }
        public DbSet<TblRequestApprovalDetail> TblRequestApprovalDetails { get; set; }
        public DbSet<LookupVehicles> LookupVehicles { get; set; }

        public DbSet<LookupUserGroup> LookupUserGroups { get; set; }

        public DbSet<TblUserGroupDistribution> TblUserGroupDistributions { get; set; }
        public DbSet<LookupRequisitionCategory> LookupRequisitionCategorys { get; set; }

        public DbSet<LookUpFileUpload> LookUpFileUploads { get; set; }




    }
}