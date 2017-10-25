using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRequisitionApp.Models
{
    public class LoopUpRedirectPage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RedirectPageId { get; set; }       
        public int? UserGroupsId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ParameterName { get; set; }
    }
}