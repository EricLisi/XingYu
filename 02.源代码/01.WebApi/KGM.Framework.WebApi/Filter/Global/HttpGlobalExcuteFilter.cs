using KGM.Framework.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace KGM.Framework.WebApi
{
    /// <summary>
    /// 全局执行处理
    /// </summary>
    public class HttpGlobalExcuteFilter : ActionFilterAttribute
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// 全局异常过滤器
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public HttpGlobalExcuteFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        /// <summary>
        /// 执行前处理
        /// </summary>
        /// <param name="context"></param>

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            string info = $"Url:【{request.Path}】 Method:【{request.Method}】 Parameter【{request.QueryString}】";
            LoggerHelper.Instance().Info(info);
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 执行后处理
        /// </summary>
        /// <param name="context"></param>

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
