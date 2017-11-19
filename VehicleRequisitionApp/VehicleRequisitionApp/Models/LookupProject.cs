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
        public int? SortingSerialNo { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPl { get; set; }
        public string ProjectType { get; set; }
        public string ProjectCluster { get; set; }
        public string ProjectClient { get; set; }                
        public string Exclude{ get; set; }
        
        public virtual List<LookupProjectLeader> LookupProjectLeaders { get; set; }        
        public virtual List<TblRequisitionDetail> TblRequisitionDetails { get; set; }
    }
}