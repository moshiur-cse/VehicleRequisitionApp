using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookUpDivision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        
        public int DivisionId { get; set; }
        [Display(Name = "Division Short Name")]
        public string DivShortName { get; set; }
        [Display(Name = "Division")]
        public string DivFullName { get; set; }
        public virtual List<LookUpEmployee> LookUpEmployees { get; set; }

    }
}