using API.Repository;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace API.Utility
{
    public sealed class TokenValidation : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {



        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var value))
            {
                if (!TokenBusiness.Validation(value.ToString().Replace("Bearer ", "")))
                {
                    var res = new { Message = "Unauthorized Access" };
                    context.Result = new CustomUnauthorizedResult(res);
                    return;
                }

            }
            else
            {
                var res = new { Message = "Unauthorized Access" };
                context.Result = new CustomUnauthorizedResult(res);
                return;
            }
        }
    }

    public class CustomUnauthorizedResult : ObjectResult
    {
        public CustomUnauthorizedResult(object value) : base(value)
        {
            StatusCode = 401;
        }
    }
}
