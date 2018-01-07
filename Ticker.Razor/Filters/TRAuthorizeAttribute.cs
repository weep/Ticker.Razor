using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker.Razor.Filters
{
    public class TRAuthorizeAttribute : Attribute, IPageFilter
    {
        private Guid _guid;
        private Stopwatch _sw;

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            _sw.Stop();
            System.Diagnostics.Debug.WriteLine($"{_guid} took {_sw.ElapsedMilliseconds}ms");
            //throw new NotImplementedException();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            _guid = Guid.NewGuid();
            _sw = Stopwatch.StartNew();
            System.Diagnostics.Debug.WriteLine($"{context.HttpContext.User} {context.HttpContext.User.Identity.IsAuthenticated}");
            //throw new NotImplementedException();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
