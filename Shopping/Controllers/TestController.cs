using Microsoft.AspNetCore.Mvc;
using Shopping.Sitemap;

namespace Shopping.Controllers;

[ControllerSitemap]
public class TestController : Controller
{
    // this overrides the previous SitemapAttribute above
    [ActionSitemap]
    [Route("test")]
    public IActionResult Index()
    {
        return View();
    }
}

public class ControllerSitemapAttribute : SitemapAttribute
{
}

public class ActionSitemapAttribute : SitemapAttribute
{
    public ActionSitemapAttribute()
    {
        ChangeFrequency = DotnetSitemapGenerator.ChangeFrequency.Daily;
        Priority = 0.9m;
    }
}