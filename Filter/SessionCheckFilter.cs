using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dot_Net_ECommerceWeb.Service.Filter;

public class SessionCheckFilter:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;
        var username = session.GetString("username");

        if (string.IsNullOrEmpty(username))
        {
            // Nếu không có username trong session, chuyển hướng đến trang login
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }

        base.OnActionExecuting(context);
    }
}