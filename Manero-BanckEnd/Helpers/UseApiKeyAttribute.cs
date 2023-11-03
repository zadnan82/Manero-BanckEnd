using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Buffers.Text;
using System.Text.Json;
using Base64 = Manero_BanckEnd.Helpers.Base64;

namespace Manero_BanckEnd.Helpers
{

    public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
    {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
            //var apiKey = config.GetValue<string>("ApiKey");


            var apiKeyRepo = context.HttpContext.RequestServices.GetService<ApiKeyRepo>();

            if (context.HttpContext.Request.Query.TryGetValue("code", out var code))
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var apiKey = JsonSerializer.Deserialize<KeyEntity>(Base64.Decode(code!));
                    if (apiKey != null)
                    {
                        if (await apiKeyRepo.ExistAsync(x => x.UserId == apiKey.UserId && x.Key == code.ToString()))
                            await next();
                    }
                }
            }


            await next();
        }
    }
}
