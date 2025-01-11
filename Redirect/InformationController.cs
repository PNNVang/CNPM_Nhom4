using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("Information")]
public class InformationController:Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("Policy")]
    public IActionResult policy()
    {
        return View("~/Views/views/policy.cshtml");
    }
    [HttpGet("About")]
    public IActionResult about()
    {
        return View("~/Views/views/about.cshtml");
    }
}