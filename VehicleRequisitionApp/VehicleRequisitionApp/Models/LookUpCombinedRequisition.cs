using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookUpCombinedRequisition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CombinedRequisitionId { get; set; }
        public int RequisitionId { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual TblRequisitionDetail TblRequisitionDetails { get; set; }

        public int AssignId { get; set; }
    }
}