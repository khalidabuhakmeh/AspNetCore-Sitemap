using DotnetSitemapGenerator;

namespace Shopping.Sitemap.Providers;

public interface ISitemapUrlProvider
{
    Task<IReadOnlyCollection<SitemapNode>> GetNodes();
}