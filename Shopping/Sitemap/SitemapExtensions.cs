using Shopping.Sitemap.Providers;

namespace Shopping.Sitemap;

public static class SitemapExtensions
{
    public static RouteHandlerBuilder MapSitemap(this IEndpointRouteBuilder endpoints, string path = "sitemap.xml")
    {
        return endpoints.MapGet(path, async (SitemapBuilder sitemap) =>
        {
            var xml = await sitemap.GenerateAsync();
            return Results.Stream(xml, "text/xml");
        });
    }

    public static void AddSitemap(this IServiceCollection services)
    {
        // add sitemap services
        services.AddScoped<ISitemapUrlProvider, PagesSitemapUrlProvider>();
        services.AddScoped<ISitemapUrlProvider, EndpointsSitemapSitemapUrlProvider>();
        services.AddScoped<ISitemapUrlProvider, ProductSitemapSitemapUrlProvider>();
        services.AddScoped<SitemapBuilder>();
    }

    public static RouteHandlerBuilder WithSitemap(this RouteHandlerBuilder endpoint,
        string name, object? defaults = null)
    {
        return endpoint
            // adds RouteName and EndpointName
            .WithName(name)
            .WithMetadata(new SitemapAttribute
            {
                RouteValues = new RouteValueDictionary(defaults)
            });
    }
}