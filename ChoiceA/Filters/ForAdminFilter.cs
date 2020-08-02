using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChoiceA.Filters
{
    public class ForAdminFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole("admin"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
