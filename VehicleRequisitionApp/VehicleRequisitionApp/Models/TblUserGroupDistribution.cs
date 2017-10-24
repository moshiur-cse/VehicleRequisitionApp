using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class TblUserGroupDistribution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupDistributionId { get; set; }
        public int UserGroupsId { get; set; }

        [ForeignKey("UserGroupsId")]
        public virtual LookupUserGroup LookupUserGroup { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual TblUser TblUser { get; set; }
    }
}