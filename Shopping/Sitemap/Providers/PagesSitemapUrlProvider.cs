using DotnetSitemapGenerator;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using IsRazorPage = Microsoft.AspNetCore.Mvc.RazorPages.CompiledPageActionDescriptor;
using IsMvc = Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

namespace Shopping.Sitemap.Providers;

public class PagesSitemapSitemapUrlProvider : ISitemapUrlProvider
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly ILogger<PagesSitemapSitemapUrlProvider> _logger;

    public PagesSitemapSitemapUrlProvider(
        LinkGenerator linkGenerator,
        IHttpContextAccessor httpContextAccessor,
        IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
        ILogger<PagesSitemapSitemapUrlProvider> logger)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        _logger = logger;
    }

    public Task<IReadOnlyCollection<SitemapNode>> GetNodes()
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        var nodes = new List<SitemapNode>();

        foreach (var descriptor in _actionDescriptorCollectionProvider.ActionDescriptors.Items)
        {
            // LastOrDefault is used to get the closest SitemapAttribute to the endpoint
            var exists = descriptor.EndpointMetadata.LastOrDefault(em => em is SitemapAttribute); 
            if (exists is not SitemapAttribute sitemapAttribute) continue;
            
            var url = descriptor switch
            {
                // Razor Pages
                IsRazorPage razorPage =>
                    _linkGenerator.GetUriByPage(httpContext, page: razorPage.ViewEnginePath),
                // ASP.NET Core MVC
                IsMvc controller =>
                    _linkGenerator.GetUriByAction(httpContext,
                        action: controller.ActionName,
                        controller: controller.ControllerName,
                        values: controller.RouteValues),
                _ => null
            };

            if (ShouldAddUrl(nodes, url))
            {
                nodes.Add(new SitemapNode(url)
                {
                    ChangeFrequency = sitemapAttribute.ChangeFrequency,
                    Priority = sitemapAttribute.Priority
                });
            }
        }

        return Task.FromResult<IReadOnlyCollection<SitemapNode>>(nodes);
    }
    
    private static bool ShouldAddUrl(List<SitemapNode> nodes, string? url)
    {
        // if the url failed to generate, don't add a record
        if (string.IsNullOrWhiteSpace(url)) return false;
        // if it already exists based on the URL, don't add it
        return !nodes.Exists(n => n.Url.Equals(url, StringComparison.OrdinalIgnoreCase));
    }
}