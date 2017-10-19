using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class TblManagementDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagementDetailId { get; set; }
        public int SortingSerialNo { get; set; }
        public int ManagementTypeId { get; set; }
        [ForeignKey("ManagementTypeId")]
        public virtual LookUpManagementType LookUpManagementType { get; set; }
        public int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual LookUpEmployee LookUpEmployee { get; set; }
    }
}