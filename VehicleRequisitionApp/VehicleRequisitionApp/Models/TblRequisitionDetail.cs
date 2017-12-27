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
        [Required(ErrorMessage = "Please select  date time ")]
        public DateTime RequiredFromDate { get; set; }
        [Display(Name = "To")]
        [Required(ErrorMessage = "Please select  date time ")]
        public DateTime RequiredToDate { get; set; }
        [Required]
        public string Place { get; set; }

        public string Reason { get; set; }
        [Display(Name = "Used From KM")]
        public double? UsedFromKM { get; set; }
        [Display(Name = "Used To KM")]
        public double? UsedToKM { get; set; }
       
        [Display(Name = "Actually Used From")]
        public DateTime? ActuallyUsedFromDate { get; set; }
        [Display(Name = "Actually Used To")]
        public DateTime? ActuallyUsedToDate { get; set; }


        [Display(Name = "Assigned Driver")] 
        public int? AssignedDriverEmpId { get; set; }
        [ForeignKey("AssignedDriverEmpId")]
        public virtual LookUpEmployee LookUpDriverEmployee { get; set; }

        [Display(Name = "Assigned Vehicle")]
        public int? AssignedVehicleId { get; set; }
        [ForeignKey("AssignedVehicleId")]
        public virtual LookupVehicles LookupVehicles { get; set; }


        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual LookUpState LookUpState {get; set; }

        public int? AssignId { get; set; }

        public virtual List<TblRequestApprovalDetail> TblRequestApprovalDetail { get; set; }
        public virtual List<LookUpCombinedRequisition> LookUpCombinedRequisition { get; set; }
     
    }

    public class CombinedRequisition
    {
        public int AssignId { get; set; }

        public DateTime RequiredFromDate { get; set; }
        public DateTime RequiredToDate { get; set; }

        public string Place { get; set; }
        public string EmpFullName { get; set; }
        public string EmpMobile { get; set; }
        public string DriverName { get; set; }
        public string DriverPhoneNo { get; set; }
        public string VehicleNo { get; set; }
        public string ProjectCode { get; set; }      
    }
    public class Prediction
    {
        public string description { get; set; }
        public string id { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public List<string> types { get; set; }
        //public Dictionary<string, string> structured_formatting { get; set; }
        //public List<List<LocationName>> structured_formatting { get; set; }
        //http://michaelcummings.net/mathoms/using-a-custom-jsonconverter-to-fix-bad-json-results/
    }
    
    //public class Location
    //{
    //    public string main_text { get; set; }
    //}

    public class RootObject
    {
        public List<Prediction> predictions { get; set; }
        public string status { get; set; }
    }
}