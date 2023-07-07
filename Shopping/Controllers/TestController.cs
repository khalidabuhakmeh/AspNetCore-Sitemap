using Microsoft.AspNetCore.Mvc;
using Shopping.Sitemap;

namespace Shopping.Controllers;

[ControllerSitemap]
public class TestController : Controller
{
    private const decimal ControllerPriority = 1;
    private const decimal ActionPriority = 2;
    
    [ActionSitemap]
    [Route("test")]
    public IActionResult Index()
    {
        return View();
    }
}

public class ControllerSitemapAttribute : SitemapAttribute
{
    public const string Name = nameof(ControllerSitemapAttribute);
}

public class ActionSitemapAttribute : SitemapAttribute
{
    public ActionSitemapAttribute()
    {
        ChangeFrequency = DotnetSitemapGenerator.ChangeFrequency.Daily;
    }
    
    public const string Name = nameof(ActionSitemapAttribute);
}