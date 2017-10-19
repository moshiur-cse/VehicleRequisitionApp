using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookupVehicles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehiclesId { get; set; }
        public int SortingSerialNo { get; set; }
        public string VehicleNo { get; set; }
        public string LicenseNo { get; set; }
        public string VehicleModel { get; set; }        
    }
}