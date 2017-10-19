using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class TblRequisitionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequisitionId { get; set; }

        public int RequisitionCategoryId { get; set; }

        [ForeignKey("RequisitionCategoryId")]
        public virtual LookupRequisitionCategory LookupRequisitionCategorys { get; set; }

        public int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual LookUpEmployee LookUpEmployee { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual LookupProject LookupProject { get; set; }

        public DateTime RequestSubmissionDate { get; set; }
        public DateTime RequiredFromDate { get; set; }
        public DateTime RequiredToDate { get; set; }

        public string Place { get; set; }
        public string Reason { get; set; }
        public DateTime ActuallyUsedFromDate { get; set; }
        public DateTime ActuallyUsedToDate { get; set; }

        public int AssignedDriverEmpId { get; set; }
        public int AssignedVehicleId { get; set; }
        
        
    }
}