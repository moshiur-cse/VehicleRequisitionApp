using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookupUserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupsId { get; set; }
        public string UserGroupName { get; set; }
        public virtual List<TblUserGroupDistribution> TblUserGroupDistributions { get; set; }
    }
}