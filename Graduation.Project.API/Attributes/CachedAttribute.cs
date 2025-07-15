using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace GP.Focusi.API.Attributes
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly double _expireTime;
        private readonly string _classType;

        public CachedAttribute(double expireTime)
		{
			_expireTime = expireTime;
        }
        public CachedAttribute(double expireTime, string classType)
        {
            _expireTime = expireTime;
            _classType = classType;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
			var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

			var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

			if(!string.IsNullOrEmpty(cacheResponse))
			{
				var contentRes = new ContentResult()
				{
					Content = cacheResponse,
					ContentType = "application/json",
					StatusCode = 200
				};
				context.Result = contentRes;
				return;
			}
			
			var executedContext = await next();

			if (executedContext.Result is OkObjectResult response)
			{
				await cacheService.SetChachedResponseAsync(cacheKey, response.Value,TimeSpan.FromDays(_expireTime));
			}
		}
		 
		private string GenerateCacheKeyFromRequest(HttpRequest request) 
		{		
			//var child = _userManager.FindByEmailAsync

			var cacheKey = new StringBuilder();
			cacheKey.Append($"{request.Host}:{request.Path}");
			cacheKey.Append(_classType);

			foreach (var (key, value) in request.Query.OrderBy(X => X.Key)) 
			{
				cacheKey.Append($"|{key} - {value}");
			}
			return cacheKey.ToString();
		}
	}
}
