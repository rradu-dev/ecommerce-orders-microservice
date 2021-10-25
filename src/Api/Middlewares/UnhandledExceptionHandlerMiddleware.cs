using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ecommerce.Services.Orders.Core.Exceptions;

namespace Ecommerce.Services.Orders.Api.Middlewares
{
	public class UnhandledExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(
			HttpContext context,
			RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
				System.Console.WriteLine(exception.Message);
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(
			HttpContext context,
			Exception exception)
        {
			object body = null;
			var response = string.Empty;

            switch(exception)
            {
				case ValidationException e:
					body = new
					{
						code = e.Code,
						message = e.Message,
						errors = e.Errors
					};
					context.Response.StatusCode =
						(int)HttpStatusCode.BadRequest;
					break;
				case NotFoundException e:
					body = new
					{
						code = e.Code,
						message = e.Message
					};
					context.Response.StatusCode =
						(int)HttpStatusCode.NotFound;
					break;
				default:
					body = new
					{
						code = "error",
						message = "There was an error."
					};
					context.Response.StatusCode =
						(int)HttpStatusCode.InternalServerError;
					break;
            }

			response = JsonConvert.SerializeObject(body);
            context.Response.ContentType = "application/json";
			return context.Response.WriteAsync(response);
        }
    }
}
