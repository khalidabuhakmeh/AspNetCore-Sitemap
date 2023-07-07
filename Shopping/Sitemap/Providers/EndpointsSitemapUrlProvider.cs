using DotnetSitemapGenerator;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Shopping.Sitemap.Providers;

public class EndpointsSitemapSitemapUrlProvider : ISitemapUrlProvider
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
    private readonly ILogger<PagesSitemapSitemapUrlProvider> _logger;

    public EndpointsSitemapSitemapUrlProvider(
        LinkGenerator linkGenerator,
        IHttpContextAccessor httpContextAccessor,
        IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
        ILogger<PagesSitemapSitemapUrlProvider> logger)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
        _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
        _logger = logger;
    }

    public Task<IReadOnlyCollection<SitemapNode>> GetNodes()
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        var nodes = new List<SitemapNode>();

        // Minimal Apis that might return HTML
        foreach (var group in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
        {
            var endpoints =
                group
                    .Items
                    .Where(i => HttpMethods.IsGet(i.HttpMethod ?? ""))
                    .Where(i => i.ActionDescriptor.EndpointMetadata.Any(em => em is SitemapAttribute));

            foreach (var endpoint in endpoints)
            {
                var attribute = endpoint
                    .ActionDescriptor
                    .EndpointMetadata
                    .LastOrDefault(a => a is SitemapAttribute);

                if (attribute is not SitemapAttribute sitemapAttribute)
                    continue;

                var routeName = endpoint
                    .ActionDescriptor
                    .EndpointMetadata
                    .Where(m => m is RouteNameMetadata)
                    .Cast<RouteNameMetadata>()
                    .Select(a => a.RouteName)
                    .FirstOrDefault();

                if (routeName is null)
                    continue;

                var url = _linkGenerator.GetUriByName(
                    httpContext,
                    routeName,
                    values: sitemapAttribute.RouteValues
                );

                if (ShouldAddUrl(nodes, url))
                {
                    nodes.Add(new SitemapNode(url)
                    {
                        ChangeFrequency = sitemapAttribute.ChangeFrequency,
                        Priority = sitemapAttribute.Priority
                    });
                }
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