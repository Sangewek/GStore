using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace GameStore.WebApi.Infrastructure
{
    public class ActionFilters : IActionFilter
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static Stopwatch _timer = new Stopwatch();
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Info("Request for " + context.ActionDescriptor.DisplayName + " was from ip: "+context.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress);
            _timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            _logger.Info("Time spend for "+context.ActionDescriptor.DisplayName+" func was "+_timer.ElapsedMilliseconds+" ms");
        }
    }
}
