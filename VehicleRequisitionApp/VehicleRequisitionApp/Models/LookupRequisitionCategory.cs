using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookupRequisitionCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequisitionCategoryId { get; set; }
        public int SortingSerialNo { get; set; }
        public string RequisitionCategory { get; set; }

        public virtual List<TblRequisitionDetail> TblRequisitionDetails { get; set; }
    }
}