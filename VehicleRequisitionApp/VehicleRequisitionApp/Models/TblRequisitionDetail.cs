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
        [Display(Name = "Requisition Category")]
        public int RequisitionCategoryId { get; set; }

        [ForeignKey("RequisitionCategoryId")]
        public virtual LookupRequisitionCategory LookupRequisitionCategorys { get; set; }
        [Display(Name = "Initial")]
        public int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual LookUpEmployee LookUpEmployee { get; set; }

        [Display(Name = "Project Code")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual LookupProject LookupProject { get; set; }

        [Display(Name = "Submission Date")]
        public DateTime RequestSubmissionDate { get; set; }

        [Display(Name = "From")]
        public DateTime RequiredFromDate { get; set; }
        [Display(Name = "To")]
        public DateTime RequiredToDate { get; set; }
        [Required]
        public string Place { get; set; }

        public string Reason { get; set; }

        public double? UsedFromKM { get; set; }
        public double? UsedToKM { get; set; }
        [Display(Name = "From")]
        public DateTime? ActuallyUsedFromDate { get; set; }
        [Display(Name = "To")]
        public DateTime? ActuallyUsedToDate { get; set; }


        [Display(Name = "Assigned Driver")] 
        public int? AssignedDriverEmpId { get; set; }

        [ForeignKey("AssignedDriverEmpId")]
        public virtual LookUpEmployee LookUpDriverEmployee { get; set; }

        [Display(Name = "Assigned Vehicle")]
        public int? AssignedVehicleId { get; set; }
        [ForeignKey("AssignedVehicleId")]
        public virtual LookupVehicles LookupVehicles { get; set; }



    }
}