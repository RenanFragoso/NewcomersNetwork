using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Areas.NNAdmin
{
    public class NNAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NNAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NNAdmin_default",
                "NNAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "NewcomersNetworkIFACE.Areas.NNAdmin.Controllers" }
            );
        }
    }
}