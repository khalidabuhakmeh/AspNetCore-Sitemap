using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Models;

namespace Shopping.Pages;

public class Products : PageModel
{
    private readonly Database _db;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? Slug { get; set; }
    
    public Product? Product { get; set; }

    public Products(Database db)
    {
        _db = db;
    }
    
    public async Task<IActionResult> OnGet()
    {
        Product = await _db.Products.FindAsync(Id);

        if (Product is null)
            return NotFound();

        // let's keep it consistent
        if (Slug != Product.Slug)
            return RedirectToPage("Products", new { Id, Product.Slug });

        // let's render the page
        return Page();
    }
}