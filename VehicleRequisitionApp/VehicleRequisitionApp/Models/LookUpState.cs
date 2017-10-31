using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookUpState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        public string StateName { get; set;}
        public virtual List<TblRequisitionDetail> TblRequisitionDetails { get; set; }

    }
}