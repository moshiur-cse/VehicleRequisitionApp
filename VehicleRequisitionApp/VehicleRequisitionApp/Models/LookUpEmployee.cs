using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookUpEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }
        public int? SortingSerialNo { get; set; }
        [Display(Name = "Pin No")]
        public string EmpPinNo { get; set; }

        public int EmpTypeId { get; set; }
        [ForeignKey("EmpTypeId")]
        public virtual LookUpEmployeeType LookUpEmployeeType { get; set; }       

        //[DataType(DataType.Password)]

        //[Required]
        [Required(ErrorMessage = "Please Enter Initial")]
        [Display(Name = "Initial")]
        [Column(TypeName = "varchar")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string EmpInitial { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        [Display(Name = "Full Name")]
        [Column(TypeName = "varchar")]
        public string EmpFullName { get; set; }
        public int EmpDivisionId { get; set; }

        [ForeignKey("EmpDivisionId")]        
        public virtual LookUpDivision LookUpDivision { get; set; }


        [Required(ErrorMessage = "Please Enter Employee Designation")]
        [Display(Name = "Designation")]
        [Column(TypeName = "varchar")]
        public string EmpDesignation { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Level")]
        [Display(Name = "Level")]
        [Column(TypeName = "varchar")]
        public string EmpLevel { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmpEmail { get; set; }

        public string EmpMobile { get; set; }
        public string EmpRoomNo { get; set; }
        public string EmpPresentAddress { get; set; }
        public string EmpNid { get; set; }
        public string EmpPaasportNo { get; set; }
        public string EmpBloodGroup { get; set; }        
        public string EmpHighestDegree { get; set; }
        public string EmpHighestDegreeMajorSubject { get; set; }
        public string EmpCareerSummary { get; set; }

        public virtual List<TblDirector> TblDirectors { get; set; }
        public virtual List<TblManagementDetail> TblManagementDetails { get; set; }
        public virtual List<LookupProjectLeader> LookupProjectLeaders { get; set; }
        public virtual List<TblUser> TblUsers { get; set; }
        public virtual List<TblRequisitionDetail> TblRequisitionDetails { get; set; }

        




    }
}