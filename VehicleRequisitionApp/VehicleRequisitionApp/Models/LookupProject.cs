using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LookupProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public int SortingSerialNo { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string ProjectTitle { get; set; }
        public string ClientName { get; set; }
        public DateTime ProjectStartingDate { get; set; }
        public DateTime ProjectEndingDate { get; set; }
        public virtual List<LookupProjectLeader> LookupProjectLeaders { get; set; }        
        public virtual List<TblRequisitionDetail> TblRequisitionDetails { get; set; }
    }
}