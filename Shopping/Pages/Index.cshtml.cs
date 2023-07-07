using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Sitemap;

namespace Shopping.Pages;

[Sitemap]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Database _db;

    public IndexModel(ILogger<IndexModel> logger, Database db)
    {
        _logger = logger;
        _db = db;
    }

    public List<Product> Products { get; set; } = new();

    public async Task OnGet()
    {
        Products = await _db
            .Products
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
    
    public async Task OnPostProduct()
    {
        
    }
}
