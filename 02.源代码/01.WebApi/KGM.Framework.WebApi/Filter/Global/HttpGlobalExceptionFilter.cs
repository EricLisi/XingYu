using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace KGM.Framework.WebApi
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// 全局异常过滤器
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public HttpGlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }
         
        /// <summary>
        /// 发生异常时处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            context.Exception.Message);

            var json = new ErrorResponse(context.Exception.Message);
            if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;
            context.Result = new ApplicationErrorResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public class ApplicationErrorResult : ObjectResult
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="value"></param>
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// 错误响应类
        /// </summary>
        public class ErrorResponse
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="msg"></param>
            public ErrorResponse(string msg)
            {
                Message = msg;
            }

            /// <summary>
            /// 消息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 开发环境消息
            /// </summary>
            public object DeveloperMessage { get; set; }
        }
    }
}
