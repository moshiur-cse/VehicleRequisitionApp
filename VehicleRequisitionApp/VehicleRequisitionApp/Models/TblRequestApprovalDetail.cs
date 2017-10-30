using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class TblRequestApprovalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestApprovalDetailId { get; set; }


        public int RequisitionId { get; set; }

        //[ForeignKey("RequisitionId")]
        //public virtual TblRequisitionDetail TblRequisitionDetail { get; set; }

        public int? ApprovalAuthorityId { get; set; }

        //[ForeignKey("ApprovalAuthorityId")]
        //public virtual LookUpEmployee LookUpEmployee { get; set; }

        public int? ApprovalStatus { get; set; }

        public string Comments { get; set; }
            
    }
}