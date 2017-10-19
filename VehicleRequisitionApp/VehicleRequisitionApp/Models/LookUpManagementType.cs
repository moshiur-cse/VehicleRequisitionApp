using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookUpManagementType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagementTypeId { get; set; }
        public int SortingSerialNo { get; set; }
        public string ManagementType { get; set; }

        public virtual List<TblManagementDetail> TblManagementDetail { get; set; }
    }
}