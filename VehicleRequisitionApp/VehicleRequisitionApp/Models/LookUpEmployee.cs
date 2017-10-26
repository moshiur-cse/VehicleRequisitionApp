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
        [Index("PinNo", IsUnique = true)]
        [StringLength(50, MinimumLength = 3)]
        public string EmpPinNo { get; set; }
        [Display(Name = "Employee Type")]
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
        [Display(Name = "Name")]
        [Column(TypeName = "varchar")]

        public string EmpFullName { get; set; }
        [Display(Name = "Division")]
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
        [Display(Name = "Phone No")]
        public string EmpMobile { get; set; }
        [Display(Name = "Room No")]
        public string EmpRoomNo { get; set; }
        [Display(Name = "Present Address")]
        public string EmpPresentAddress { get; set; }
        [Display(Name = "NID No")]
        public string EmpNid { get; set; }
        [Display(Name = "Passport No")]
        public string EmpPaasportNo { get; set; }
        [Display(Name = "Blood Group")]
        public string EmpBloodGroup { get; set; }
        [Display(Name = "Highest Degree")]
        public string EmpHighestDegree { get; set; }
        [Display(Name = "Higest Degree Subject")]
        public string EmpHighestDegreeMajorSubject { get; set; }
        [Display(Name = "Career Summary")]
        public string EmpCareerSummary { get; set; }

        public virtual List<TblDirector> TblDirectors { get; set; }
        public virtual List<TblDriver> TblDrivers { get; set; }

        
        public virtual List<TblManagementDetail> TblManagementDetails { get; set; }
        public virtual List<LookupProjectLeader> LookupProjectLeaders { get; set; }
        public virtual List<TblUser> TblUsers { get; set; }
        public virtual List<TblRequisitionDetail> TblRequisitionDetails { get; set; }

        




    }
}