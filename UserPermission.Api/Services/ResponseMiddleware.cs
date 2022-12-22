using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;

namespace UserPermission.Api.Services
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISaveOperationService saveOperationService, ILogger<ResponseMiddleware> logger)
        {
            await _next(context);
            var uri = context.Request.Path.ToString().Split("/");
            int position = uri.Length - 1;
            var method = uri[position];
            if (!method.Equals("MoveNext") && !string.IsNullOrEmpty(method)) {
                await saveOperationService.Save(new Operation { Name = method });
                logger.LogInformation($"Ingresando a {method}");
            }
        }
    }
}
