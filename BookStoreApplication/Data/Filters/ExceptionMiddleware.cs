
using BookStoreApplicationAPI.Data.Exceptions;
using BookStoreApplicationAPI.Data.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BookStoreApplicationAPI.Filters
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _hostEnvironment = hostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException domainException)
            {
                await HandleDomainException(context, domainException);
            }
            catch (Exception exception)
            {
                //await HandleException(context, exception);
            }
        }

        //public void HandleException(HttpContext context, Exception exception)
        //{
           
        //    var error = new ApiError();
        //    if (_hostEnvironment.IsDevelopment())
        //    {
        //        error.Message = context.Exception.Message;
        //        error.Detail = context.Exception.StackTrace;
        //    }
        //    else
        //    {
        //        error.Message = "ser error occured.";
        //        error.Detail = context.Exception.Message;
        //        context.Result = new ObjectResult(error)
        //        {
        //            StatusCode = 500
        //        };
        //    }
        //}
        private async Task HandleDomainException(HttpContext context, DomainException domainException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = domainException.ErrorCode;
            ErrorDescription value = new ErrorDescription(Guid.NewGuid(), domainException.ErrorMessage, domainException.ErrorCode);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(value));
            //ErrorDescription errorDescription = new ErrorDescription(Guid.NewGuid(), _hostEnvironment.IsDevelopment() ? exception.ToString() : "An error occurred in the API. Please use the id and contact the support team if the problem persists.", 500);
            //await context.Response.WriteAsync(JsonConvert.SerializeObject(_hostEnvironment.IsDevelopment() ? "reeEror dev" : "rerrr nodev"));
            //_logger.LogError(exception, "An exception with id: {@ErrorId} was caught in the API request pipeline.", errorDescription.Id);
        }
    }
}
