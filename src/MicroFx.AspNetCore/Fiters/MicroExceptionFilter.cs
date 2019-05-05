using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MicroFx.AspNetCore.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicroFx.AspNetCore.Fiters
{
    public class MicroExceptionFilter : IExceptionFilter
    {
        IHostingEnvironment _env;
        public MicroExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            if (_env.IsDevelopment())
            {
                context.Result = new InternalServerErrorResult(new
                {
                    Msg = "服务器内部错误",
                    DevMsg = context.Exception
                });
            }
            else
            {
                context.Result = new InternalServerErrorResult(new
                {
                    Msg = "服务器内部错误",
                });
            }
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}
