using System.Web.Http;
using System.Web.Http.Results;

namespace NewcomersNetworkAPI.Extensions
{
    public static class ApiControllerExtensions
    {
        public static IHttpActionResult ReturnFormError(this ApiController controller, System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            return new InvalidModelStateResult(modelState, controller);
        }
    }
}