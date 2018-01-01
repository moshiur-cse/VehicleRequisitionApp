using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VehicleRequisitionApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TblUsers", action = "LogIn", id = "" } //UrlParameter.Optional
            );
            //routes.MapRoute(
            //    name: "ItemLinked",
            //    //url: "RequisitionDetails/{RequisitionId}/{controller}/{action}/{id}",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "TblRequisitionDetails", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { RequisitionId = "^[0-9]*$", id = "^[0-9]*$" }
            //    );

        }
    }
}
//@Html.RouteLink("Go Back", "ItemLinked", new {
//        controller = "SubItems",
//        action = "Index", 
//        itemId = ViewBag.itemid,
//    })

//     {
//                       controller = "TblRequisitionDetails",
//                       action = "Index",
//                       id = Session["EmpId"]
//                   })