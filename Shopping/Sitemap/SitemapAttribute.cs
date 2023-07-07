using DotnetSitemapGenerator;
using DotnetSitemapGenerator.Images;
using DotnetSitemapGenerator.News;

namespace Shopping.Sitemap;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SitemapAttribute : Attribute
{
    
    public RouteValueDictionary? RouteValues { get; set; }
    public DateTime? LastModificationDate { get; set; }
    public ChangeFrequency? ChangeFrequency { get; set; }
    public decimal? Priority { get; set; }
}

