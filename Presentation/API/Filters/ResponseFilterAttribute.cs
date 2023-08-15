using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ResponseFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            if (context.Result is ObjectResult objectResult)
            {
                Response<object> result = new Response<object>()
                {
                    Data = objectResult.Value,
                    Errors = null,
                    Message = null,
                    Succeeded = true
                };

                context.Result = new OkObjectResult(result);
            }
        }
    }
}
