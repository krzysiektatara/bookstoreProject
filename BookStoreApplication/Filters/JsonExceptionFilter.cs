﻿using BookStoreApplicationAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BookStoreApplicationAPI.Filters
{
    public class ExceptionMiddleware
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _hostEnvironment = env;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            if (_hostEnvironment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "ser error occured.";
                error.Detail = context.Exception.Message;
                context.Result = new ObjectResult(error)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
