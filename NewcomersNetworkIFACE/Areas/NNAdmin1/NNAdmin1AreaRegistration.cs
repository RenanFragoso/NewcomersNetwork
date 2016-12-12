using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1
{
    public class NNAdmin1AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NNAdmin1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NNAdmin1_default",
                "NNAdmin1/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers" }
            );
        }
    }
}