using ChoiceA.ActionResults;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChoiceA.Filters
{
    class RedBeautyFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as BeautyResult;
            result.InnerHtml = $"<div style='color: red'>{result.InnerHtml}</div>";
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}
