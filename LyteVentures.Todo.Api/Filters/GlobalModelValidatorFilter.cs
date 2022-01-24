using LyteVentures.Todo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace LyteVentures.Todo.Api.Filters
{
    public class GlobalModelValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var value in context.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                context.Result = new BadRequestObjectResult(new BaseResponse
                {
                    IsSuccess = false,
                    Message = JsonConvert.SerializeObject(errors)
                });
            }
        }
    }
}
