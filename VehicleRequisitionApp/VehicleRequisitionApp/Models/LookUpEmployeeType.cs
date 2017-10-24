using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookUpEmployeeType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpTypeId { get; set; }
        public int SortingSerialNo { get; set; }
        [Display(Name = "Employee Type")]
        public string EmpType { get; set; }
        public virtual List<LookUpEmployee> LookUpEmployees { get; set; }
    }
}