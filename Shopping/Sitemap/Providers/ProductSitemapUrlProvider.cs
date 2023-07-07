using DotnetSitemapGenerator;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace Shopping.Sitemap.Providers;

public class ProductSitemapSitemapUrlProvider : ISitemapUrlProvider
{
    private readonly Database _db;
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductSitemapSitemapUrlProvider(
        Database db, 
        LinkGenerator linkGenerator, 
        IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<IReadOnlyCollection<SitemapNode>> GetNodes()
    {
        var elements = new List<SitemapNode>();
        var products = await _db.Products.OrderBy(x => x.Id).ToListAsync();
        
        foreach (var product in products)
        {
            var url = _linkGenerator.GetUriByPage(
                _httpContextAccessor.HttpContext!,
                page: "/Products",
                values: new { product.Id, product.Slug });
            
            elements.Add(new SitemapNode(url));
        }

        return elements;
    }
}